using BinanceFeed.Application.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceFeed.API.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
	public ITickerPriceRepository _tickerPriceRepository;

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		_tickerPriceRepository = A.Fake<ITickerPriceRepository>();

		builder.ConfigureServices(services =>
		{
			var tickerPriceRepository = services.SingleOrDefault(
				d => d.ServiceType == typeof(ITickerPriceRepository));

			services.Remove(tickerPriceRepository);

			services.AddScoped(x => _tickerPriceRepository);
		});

		builder.UseEnvironment("Development");
	}
}
