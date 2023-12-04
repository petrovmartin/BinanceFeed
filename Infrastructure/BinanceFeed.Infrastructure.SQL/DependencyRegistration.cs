using BinanceFeed.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BinanceFeed.Infrastructure.SQL;

public static class DependencyRegistration
{
	public static IServiceCollection AddMsSql(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<ApplicationDbContext>(
			x => x.UseSqlServer(connectionString));

		services.AddScoped<ITickerPriceRepository, TickerPriceRepository>();

		return services;
	}

	public static async Task RunDatabaseMigrations(this IServiceProvider provider)
	{
		using var scope = provider.CreateScope();
		var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
		var logger = scope.ServiceProvider.GetService<ILogger<ApplicationDbContext>>();

		logger.LogInformation("Starting migration...");
		await dbContext.Database.MigrateAsync();

		logger.LogInformation("Migration done!");
	}
}
