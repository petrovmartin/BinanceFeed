using Microsoft.AspNetCore.Mvc;

namespace BinanceFeed.API;

public class Get24hAvgRequest
{
	[FromRoute(Name = "symbol")]
	public string Symbol { get; set; }
}
