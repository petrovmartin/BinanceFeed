using BinanceFeed.API.Contracts.Responses;
using BinanceFeed.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BinanceFeed.API.Controllers;

[ApiController]
[Route("api")]
public class StatisticsController : ControllerBase
{
	private readonly ILogger<StatisticsController> _logger;
	private readonly IMediator _mediator;

	public StatisticsController(ILogger<StatisticsController> logger, IMediator mediator)
	{
		_logger = logger;
		_mediator = mediator;
	}

	[HttpGet]
	[Route("{symbol}/24hAvgPrice")]
	public async Task<ActionResult<Get24hAvgResponse>> Get24hAvg(Get24hAvgRequest req)
	{
		var result = await _mediator.Send(new TickerPriceRequest(req.Symbol));

		var response = new Get24hAvgResponse
		{
			Avg24Price = result.Avg24Price
		};

		return Ok(response);
	}

	[OutputCache]
	[HttpGet]
	[Route("{symbol}/SimpleMovingAverage")]
	public async Task<ActionResult<GetSimpleMovingAvgResponse>> GetSimpleMovingAvg(GetSimpleMovingAvgRequest req)
	{
		var result = await _mediator.Send(
			new TickerSimpleMovingAvgRequest(req.Symbol, req.NumberOfDataPoints, req.TimePeriod, req.StartDateTime));

		var response = new GetSimpleMovingAvgResponse
		{
			SimpleMovingAvgPrice = result.SimpleMovingAvgPrice
		};

		return Ok(response);
	}
}