using BinanceFeed.Domain;
using BinanceFeed.Infrastructure.SQL.Models.Entities;

namespace BinanceFeed.Infrastructure.SQL;

public static class DomainToEntityMapper
{
	public static TickerPriceEntity MapToEntity(this TickerPrice tickerPrice)
	{
		return new TickerPriceEntity
		{
			Symbol = tickerPrice.Symbol,
			Weighted24Avg = tickerPrice.Weighted24Avg,
			EventDate = tickerPrice.EventDate
		};
	}

	public static TickerPrice MapToDomain(this TickerPriceEntity tickerPrice)
	{
		return new TickerPrice
		{
			Symbol = tickerPrice.Symbol,
			Weighted24Avg = tickerPrice.Weighted24Avg,
			EventDate = tickerPrice.EventDate
		};
	}
}
