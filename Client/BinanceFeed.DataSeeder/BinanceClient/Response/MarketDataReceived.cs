namespace BinanceFeed.DataSeeder.BinanceClient.Response;

internal class BinanceEventData
{
	public string s { get; set; }
	public double w { get; set; }
	public long E { get; set; }
	public string e { get; set; }
}

internal class OnReceiveEvent
{
	public string stream { get; set; }
	public BinanceEventData data { get; set; }
}