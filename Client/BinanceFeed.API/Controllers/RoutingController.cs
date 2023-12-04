using Microsoft.AspNetCore.Mvc;

namespace BinanceFeed.API;

[ApiExplorerSettings(IgnoreApi = true)]
public class RoutingController : ControllerBase
{
	[Route("/")]
	public IActionResult Index()
	{
		return new RedirectResult("~/swagger");
	}
}
