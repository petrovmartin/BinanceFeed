namespace BinanceFeed.Infrastructure.SQL.Models.Entities;

public class TickerPriceEntity
{
	public TickerPriceEntity()
	{

	}

	public TickerPriceEntity(string symbol, double weighted24Avg, DateTime eventDate)
	{
		Symbol = symbol;
		Weighted24Avg = weighted24Avg;
		EventDate = eventDate;
	}

	public Guid Id { get; set; }

	public string Symbol { get; set; }

	public double Weighted24Avg { get; set; }

	public DateTime EventDate { get; set; }
}