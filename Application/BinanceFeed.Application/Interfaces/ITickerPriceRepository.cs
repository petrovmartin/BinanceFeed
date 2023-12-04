using BinanceFeed.Domain;

namespace BinanceFeed.Application.Interfaces;

public interface ITickerPriceRepository
{
	public Task<TickerPrice> GetLastTickerPrice(string symbol, CancellationToken token);

	public Task AddTickerPrice(TickerPrice item, CancellationToken token);

	public Task<List<TickerPrice>> GetTickerPrices(string symbol, DateTime startPeriod, DateTime endPeriod, CancellationToken token);
}