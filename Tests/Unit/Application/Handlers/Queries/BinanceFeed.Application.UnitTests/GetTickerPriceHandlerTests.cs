using BinanceFeed.Application.Interfaces;
using BinanceFeed.Domain;
using FakeItEasy;
using MediatR.Pipeline;
using Xunit;

namespace BinanceFeed.Application.UnitTests;

public class GetTickerPriceHandlerTests
{
	private readonly ITickerPriceRepository _tickerPriceRepository;
	private readonly GetTickerPriceHandler _sut;

	public GetTickerPriceHandlerTests()
	{
		_tickerPriceRepository = A.Fake<ITickerPriceRepository>();
		_sut = new GetTickerPriceHandler(_tickerPriceRepository);
	}

	[Fact(DisplayName = $"{nameof(Handle_WhenCalled_ShouldCallRepositoryOnlyOnce)}")]
	public async void Handle_WhenCalled_ShouldCallRepositoryOnlyOnce()
	{
		//arragnge
		var symbol = "ethusdt";
		var entity = new TickerPrice(symbol, 22.2, DateTime.UtcNow);

		var expectedRepoCall = A.CallTo(() => _tickerPriceRepository
			.GetLastTickerPrice(A<string>._, A<CancellationToken>._));

		var request = new TickerPriceRequest(symbol);
		var cancellationToken = new CancellationTokenSource();

		//act
		var result = await _sut.Handle(request, cancellationToken.Token);

		//assert
		expectedRepoCall.MustHaveHappenedOnceExactly();
	}

	[Fact(DisplayName = $"{nameof(Handle_WhenCalled_ShouldReturnExpectedResult)}")]
	public async void Handle_WhenCalled_ShouldReturnExpectedResult()
	{
		//arragnge
		var symbol = "ethusdt";
		var entity = new TickerPrice(symbol, 22.2, DateTime.UtcNow);

		var expectedRepoCall = A.CallTo(() => _tickerPriceRepository
			.GetLastTickerPrice(A<string>._, A<CancellationToken>._));

		expectedRepoCall.Returns(entity);

		var request = new TickerPriceRequest(symbol);
		var cancellationToken = new CancellationTokenSource();

		//act
		var result = await _sut.Handle(request, cancellationToken.Token);

		//assert
		Assert.NotNull(result);
		Assert.Equal(entity.Weighted24Avg, result.Avg24Price);
	}
}