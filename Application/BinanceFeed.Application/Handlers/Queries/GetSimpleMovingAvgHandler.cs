using BinanceFeed.Application.Interfaces;
using MediatR;

namespace BinanceFeed.Application;


public class TickerSimpleMovingAvgRequest : IRequest<TickerSimpleMovingAvgResponse>
{
	public TickerSimpleMovingAvgRequest(string symbol, int dataPoints, string timePeriod, DateTime? date)
	{
		Symbol = symbol;
		TimePeriod = timePeriod;
		DataPoints = dataPoints;
		Date = date;
	}

	public string Symbol { get; set; }
	public string TimePeriod { get; set; }
	public int DataPoints { get; set; }
	public DateTime? Date { get; set; }
}

public class TickerSimpleMovingAvgResponse
{
	public double SimpleMovingAvgPrice { get; set; }
}

public class GetSimpleMovingAvgHandler : IRequestHandler<TickerSimpleMovingAvgRequest, TickerSimpleMovingAvgResponse>
{
	private readonly ITickerPriceRepository _tickerPriceRepository;

	public GetSimpleMovingAvgHandler(ITickerPriceRepository tickerPriceRepository)
	{
		_tickerPriceRepository = tickerPriceRepository;
	}

	public async Task<TickerSimpleMovingAvgResponse> Handle(TickerSimpleMovingAvgRequest request, CancellationToken cancellationToken)
	{
		var period = ConvertTimePeriodToTime(request.TimePeriod, request.DataPoints);

		var endDate = request.Date ?? DateTime.UtcNow;
		var startDate = endDate - period;

		var result = await _tickerPriceRepository.GetTickerPrices(request.Symbol, startDate, endDate, cancellationToken);

		var avgPrice = result.DefaultIfEmpty(new()).Average(x => x.Weighted24Avg);

		return new TickerSimpleMovingAvgResponse
		{
			SimpleMovingAvgPrice = avgPrice
		};
	}

	private TimeSpan ConvertTimePeriodToTime(string timePeriod, int numberOfDataPoints)
	{
		var period = timePeriod switch
		{
			"1w" => TimeSpan.FromDays(7),
			"1d" => TimeSpan.FromDays(1),
			"30m" => TimeSpan.FromMinutes(30),
			"5m" => TimeSpan.FromMinutes(5),
			"1m" => TimeSpan.FromMinutes(1),
			_ => throw new NotSupportedException()
		};

		return period * numberOfDataPoints;
	}
}