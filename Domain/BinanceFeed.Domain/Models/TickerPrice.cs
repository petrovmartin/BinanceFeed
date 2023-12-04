namespace BinanceFeed.Domain;

public class TickerPrice
{
    public TickerPrice()
    {

    }

    public TickerPrice(string symbol, double weighted24Avg, DateTime eventDate)
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
