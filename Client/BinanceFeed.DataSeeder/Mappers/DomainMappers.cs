using BinanceFeed.DataSeeder.BinanceClient.Response;
using BinanceFeed.Domain;

namespace BinanceFeed.DataSeeder.Mappers;

internal static class DomainMappers
{
    public static TickerPrice MapClientToDomain(this OnReceiveEvent @event)
    {
        var date = DateTime.UnixEpoch.AddMilliseconds(@event.data.E);

        var mappedEvent = new TickerPrice
        (
            @event.data.s,
            @event.data.w,
            date
        );

        return mappedEvent;
    }
}