using BinanceFeed.Application.Interfaces;
using BinanceFeed.Domain;
using FakeItEasy;
using MediatR.Pipeline;
using Xunit;

namespace BinanceFeed.Application.UnitTests;

public class GetSimpleMovingAvgHandlerTests
{
	private readonly ITickerPriceRepository _tickerPriceRepository;
	private readonly GetSimpleMovingAvgHandler _sut;

	public GetSimpleMovingAvgHandlerTests()
	{
		_tickerPriceRepository = A.Fake<ITickerPriceRepository>();
		_sut = new GetSimpleMovingAvgHandler(_tickerPriceRepository);
	}

	[Fact(DisplayName = $"{nameof(Handle_WhenCalled_ShouldCallRepositoryOnlyOnce)}")]
	public async void Handle_WhenCalled_ShouldCallRepositoryOnlyOnce()
	{
		//arragnge
		var symbol = "ethusdt";
		var entity = new TickerPrice(symbol, 22.2, DateTime.UtcNow);

		var expectedRepoCall = A.CallTo(() => _tickerPriceRepository
			.GetTickerPrices(A<string>._, A<DateTime>._, A<DateTime>._, A<CancellationToken>._));

		var dataPoints = 3;
		var timePeriod = "1w";
		var todaysDate = DateTime.UtcNow;
		var request = new TickerSimpleMovingAvgRequest(symbol, dataPoints, timePeriod, DateTime.UtcNow);
		var cancellationToken = new CancellationTokenSource();

		//act
		var result = await _sut.Handle(request, cancellationToken.Token);

		//assert
		expectedRepoCall.MustHaveHappenedOnceExactly();
	}

	[Fact(DisplayName = $"{nameof(Handle_WhenCalledWith1w_ShouldPassCorrectDateRangeToRepo)}")]
	public async void Handle_WhenCalledWith1w_ShouldPassCorrectDateRangeToRepo()
	{
		//arragnge
		var symbol = "ethusdt";
		var entity = new TickerPrice(symbol, 22.2, DateTime.UtcNow);

		var expectedRepoCall = A.CallTo(() => _tickerPriceRepository
			.GetTickerPrices(A<string>._, A<DateTime>._, A<DateTime>._, A<CancellationToken>._));

		expectedRepoCall.Returns([entity]);

		var dataPoints = 3;
		var timePeriod = "1w";
		var todaysDate = DateTime.UtcNow;
		var request = new TickerSimpleMovingAvgRequest(symbol, dataPoints, timePeriod, todaysDate);
		var cancellationToken = new CancellationTokenSource();

		var endDate = request.Date ?? todaysDate;
		var startDate = endDate - (TimeSpan.FromDays(7) * request.DataPoints);

		//act
		var result = await _sut.Handle(request, cancellationToken.Token);

		//assert
		expectedRepoCall
			.WhenArgumentsMatch(x =>
				x.Get<string>(0) == symbol &&
				x.Get<DateTime>(1) == startDate &&
				x.Get<DateTime>(2) == endDate &&
				x.Get<CancellationToken>(3) == cancellationToken.Token)
			.MustHaveHappenedOnceExactly();
	}
}