using BinanceFeed.Application;
using BinanceFeed.Infrastructure.SQL;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BinanceFeed.API;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration config)
	{
		return services
			.AddAPIDependencies(config)
			.AddInfrastructureLayer(config);
	}

	private static IServiceCollection AddAPIDependencies(this IServiceCollection services, IConfiguration config)
	{
		services.AddMediatR(cfg =>
			 cfg.RegisterServicesFromAssembly(typeof(GetTickerPriceHandler).Assembly));

		services.AddOutputCache(options =>
		{
			options.AddBasePolicy(builder =>
				builder.Expire(TimeSpan.FromSeconds(20)));
		});


		services.AddStackExchangeRedisOutputCache(options =>
		{
			options.Configuration = config["RedisConfig:ConnectionString"];
			options.InstanceName = "BinanceFeedAPI";
		});

		services.AddValidatorsFromAssemblyContaining<Program>();
		services.AddFluentValidationAutoValidation();

		return services;
	}

	private static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration config)
	{
		var msSqlConfig = config["MsSqlDbConfig:ConnectionString"];
		services.AddMsSql(msSqlConfig);

		return services;
	}
}