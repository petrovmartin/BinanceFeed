using Binance.Spot;
using BinanceFeed.Application.Interfaces;
using BinanceFeed.DataSeeder.Mappers;

namespace BinanceFeed.DataSeeder;

public class Seeder : BackgroundService
{
	private readonly ILogger<Seeder> _logger;
	private readonly IServiceProvider _provider;
	private readonly IConfiguration _config;

	public Seeder(
		ILogger<Seeder> logger,
		IServiceProvider provider,
		IConfiguration config
		)
	{
		_logger = logger;
		_provider = provider;
		_config = config;
	}

	protected override async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		try
		{
			await StartSocketCommunication(cancellationToken);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
		}
		finally
		{
			if (!cancellationToken.IsCancellationRequested)
				_logger.LogError("Worker stopped unexpectedly.");
		}
	}

	private async Task StartSocketCommunication(CancellationToken cancellationToken)
	{
		var streamsToSubscribe = _config.GetSection("Streams").GetChildren().Select(section => section.Value).ToArray();
		var websocket = new MarketDataWebSocket(streamsToSubscribe);

		_logger.LogInformation("Subscribed to Streams: {streams}", String.Join(", ", streamsToSubscribe));

		websocket.OnMessageReceived(
		async (data) =>
		{
			_logger.LogInformation($"Start receiving...");

			using var scope = _provider.CreateScope();
			var repositoryService = scope.ServiceProvider.GetRequiredService<ITickerPriceRepository>();

			var trimmedData = TrimRawData(data);

			trimmedData.ThrowIfNull();

			var onReceiveEvent = trimmedData.MapRawStreamToEvent();

			var domainObj = onReceiveEvent.MapClientToDomain();

			await repositoryService.AddTickerPrice(domainObj, cancellationToken);

			_logger.LogInformation($"Saved in db successfully");
		}, cancellationToken);

		await websocket.ConnectAsync(cancellationToken);
	}

	private string TrimRawData(string rawData)
	{
		var parsed = rawData.Split("\0\0").FirstOrDefault();

		return parsed;
	}
}