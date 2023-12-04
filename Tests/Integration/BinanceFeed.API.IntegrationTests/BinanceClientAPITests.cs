using BinanceFeed.Domain;
using FakeItEasy;
using Xunit;
using System.Net.Http.Json;
using BinanceFeed.API.Contracts.Responses;

namespace BinanceFeed.API.IntegrationTests;

public class BinanceClientAPITests : IClassFixture<CustomWebApplicationFactory>
{
	private readonly CustomWebApplicationFactory _customWebApplicationFactory;
	private readonly HttpClient _binanceFeedAPIClient;

	public BinanceClientAPITests(
		CustomWebApplicationFactory customWebApplicationFactory)
	{
		_customWebApplicationFactory = customWebApplicationFactory;
		_binanceFeedAPIClient = customWebApplicationFactory.CreateClient();
	}

	[Fact(DisplayName = $"{nameof(Get24hAvg_WhenCalledWithValidData_ShouldReturnAvg24Price)}")]
	public async Task Get24hAvg_WhenCalledWithValidData_ShouldReturnAvg24Price()
	{
		//arrange
		var symbol = "ethusdt";
		var weightedPrice = 22.55;
		var todaysDate = DateTime.UtcNow;
		var entity = new TickerPrice(symbol, weightedPrice, todaysDate);

		A.CallTo(() => _customWebApplicationFactory._tickerPriceRepository.GetLastTickerPrice(
			A<string>._, A<CancellationToken>._)).Returns(entity);

		var uri = $"/api/{symbol}/24hAvgPrice";

		//act
		var response = await _binanceFeedAPIClient.GetFromJsonAsync<Get24hAvgResponse>(uri);

		//assert
		Assert.NotNull(response);
		Assert.Equal(weightedPrice, response.Avg24Price);
	}

	[Fact(DisplayName = $"{nameof(GetSimpleMovingAvg_WhenCalledWithValidData_ShouldReturnExpectedData)}")]
	public async Task GetSimpleMovingAvg_WhenCalledWithValidData_ShouldReturnExpectedData()
	{
		//arrange
		var symbol = "ethusdt";
		var weightedPriceEntityOne = 20;
		var weightedPriceEntityTwo = 30;
		var todaysDate = DateTime.UtcNow;
		var entityOne = new TickerPrice(symbol, weightedPriceEntityOne, todaysDate);
		var entityTwo = new TickerPrice(symbol, weightedPriceEntityTwo, todaysDate);

		A.CallTo(() => _customWebApplicationFactory._tickerPriceRepository.GetTickerPrices(
			A<string>._, A<DateTime>._, A<DateTime>._, A<CancellationToken>._)).Returns([entityOne, entityTwo]);


		var dataPoints = 3;
		var timePeriod = "1w";
		var dateNow = DateTime.UtcNow.Date;
		var uri = $"/api/{symbol}/SimpleMovingAverage?n={dataPoints}&p={timePeriod}&s={dateNow:yyyy-MM-dd}";

		var expectedAvg24Price = 25;

		//act
		var response = await _binanceFeedAPIClient.GetFromJsonAsync<GetSimpleMovingAvgResponse>(uri);

		//assert
		Assert.NotNull(response);
		Assert.Equal(expectedAvg24Price, response.SimpleMovingAvgPrice);
	}
}