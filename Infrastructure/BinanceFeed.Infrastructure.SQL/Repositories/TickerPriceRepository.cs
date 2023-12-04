using BinanceFeed.Application.Interfaces;
using BinanceFeed.Domain;
using BinanceFeed.Infrastructure.SQL.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BinanceFeed.Infrastructure.SQL;

public class TickerPriceRepository : ITickerPriceRepository
{
	private readonly ApplicationDbContext _dbContext;

	public TickerPriceRepository(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task AddTickerPrice(TickerPrice item, CancellationToken token)
	{
		var entry = await _dbContext
			.Set<TickerPriceEntity>()
			.AddAsync(item.MapToEntity());

		await _dbContext.SaveChangesAsync();
	}

	public async Task<TickerPrice> GetLastTickerPrice(string symbol, CancellationToken token)
	{
		var entry = await _dbContext.TickerPriceEntity
			.Where(x => x.Symbol == symbol)
			.OrderByDescending(x => x.EventDate)
			.FirstOrDefaultAsync();

		return entry.MapToDomain();
	}

	public async Task<List<TickerPrice>> GetTickerPrices(string symbol, DateTime startDate, DateTime endDate, CancellationToken token)
	{
		var entry = await _dbContext
			.Set<TickerPriceEntity>()
			.Where(x => x.EventDate > startDate && x.EventDate < endDate)
			.ToListAsync();

		return entry.Select(x => x.MapToDomain()).ToList();
	}
}