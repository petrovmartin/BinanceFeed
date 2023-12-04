using BinanceFeed.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BinanceFeed.Infrastructure.SQL;
using BinanceFeed.Console;
using MediatR;
using Microsoft.Extensions.Logging;

internal class Program
{
	private const char Space = ' ';

	private static async Task Main(string[] args)
	{
		IConfiguration configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
			.AddEnvironmentVariables()
			.Build();

		var serviceProvider = new ServiceCollection()
			.AddMsSql(configuration["MsSqlDbConfig:ConnectionString"])
			.AddMediatR(cfg =>
				 cfg.RegisterServicesFromAssembly(typeof(GetTickerPriceHandler).Assembly))
			.AddScoped<IWriter, Writer>()
			.AddScoped<IReader, Reader>()
			.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Error))
			.BuildServiceProvider();

		using var scope = serviceProvider.CreateScope();
		var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
		var reader = scope.ServiceProvider.GetRequiredService<IReader>();
		var writer = scope.ServiceProvider.GetRequiredService<IWriter>();
		var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

		while (true)
		{
			writer.Send("Enter command:");
			var input = reader.Read();
			if (input is null) continue;

			try
			{
				var response = await MapToRequest(input, mediator);
				if (response is null)
				{
					break;
				}

				writer.Send(response);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
			}
		}
	}

	private static async Task<string> MapToRequest(string readerInput, IMediator mediator)
	{
		var splitInput = readerInput.Split(Space);

		var request = splitInput switch
		{
		["24h", var symbol] => await Avg24hPriceCommandExecuter(mediator, symbol),
		["sma", var symbol, var dataPoints, var timeperiod, var date] => await SimpleMovingAvgCommandExecuter(mediator, symbol, dataPoints, timeperiod, date),
		["sma", var symbol, var dataPoints, var timePeriod] => await SimpleMovingAvgCommandExecuter(mediator, symbol, dataPoints, timePeriod, null),
		["quit"] => null,

			_ => throw new ArgumentException("Command not supported. Write 'quit' if you'd like to exit.")
		};

		return request;
	}

	private static async Task<string> Avg24hPriceCommandExecuter(IMediator mediator, string symbol)
	{
		var result = await mediator.Send(new TickerPriceRequest(symbol));

		return $"The 24h avg price is: {result.Avg24Price}";
	}

	private static async Task<string> SimpleMovingAvgCommandExecuter(IMediator mediator, string symbol, string dataPoints, string timeperiod, string date)
	{
		var result = await mediator.Send(
			new TickerSimpleMovingAvgRequest(
				symbol,
				int.Parse(dataPoints),
				timeperiod,
				date is null ? DateTime.UtcNow : DateTime.Parse(date)));

		return $"The simple moving average price is: {result.SimpleMovingAvgPrice}";
	}
}