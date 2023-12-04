using Microsoft.AspNetCore.Mvc;

namespace BinanceFeed.API;

public class GetSimpleMovingAvgRequest
{
	[FromRoute(Name = "symbol")]
	public string Symbol { get; set; }
	[FromQuery(Name = "n")]
	public int NumberOfDataPoints { get; set; }
	[FromQuery(Name = "p")]
	public string TimePeriod { get; set; }
	[FromQuery(Name = "s")]
	public DateTime? StartDateTime { get; set; }
}
