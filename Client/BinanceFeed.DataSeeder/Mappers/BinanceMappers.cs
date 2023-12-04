using BinanceFeed.DataSeeder.BinanceClient.Response;
using Newtonsoft.Json;

namespace BinanceFeed.DataSeeder;

internal static class BinanceMappers
{
	public static OnReceiveEvent MapRawStreamToEvent(this string rawEvent)
	{
		var onReceiveEvent = JsonConvert.DeserializeObject<OnReceiveEvent>(rawEvent);
		return onReceiveEvent;
	}
}
