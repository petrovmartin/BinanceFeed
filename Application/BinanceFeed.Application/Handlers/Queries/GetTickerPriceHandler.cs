using BinanceFeed.Application.Interfaces;
using MediatR;

namespace BinanceFeed.Application;

public class TickerPriceRequest : IRequest<TickerPriceResponse>
{
	public TickerPriceRequest(string symbol) => Symbol = symbol;

	public string Symbol { get; set; }
}

public class TickerPriceResponse
{
	public double Avg24Price { get; set; }
}

public class GetTickerPriceHandler : IRequestHandler<TickerPriceRequest, TickerPriceResponse>
{
	private readonly ITickerPriceRepository _tickerPriceRepository;

	public GetTickerPriceHandler(ITickerPriceRepository tickerPriceRepository)
	{
		_tickerPriceRepository = tickerPriceRepository;
	}

	public async Task<TickerPriceResponse> Handle(TickerPriceRequest request, CancellationToken cancellationToken)
	{
		var result = await _tickerPriceRepository.GetLastTickerPrice(request.Symbol, cancellationToken);

		return new TickerPriceResponse
		{
			Avg24Price = result.Weighted24Avg
		};
	}
}
