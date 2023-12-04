using FluentValidation;

namespace BinanceFeed.API;

public class GetSimpleMovingAvgRequestValidator : AbstractValidator<GetSimpleMovingAvgRequest>
{
	public GetSimpleMovingAvgRequestValidator()
	{
		RuleFor(x => x.Symbol)
			.NotEmpty()
			.Must(symbol => Shared.AcceptedSymbols.Contains(symbol.ToLowerInvariant()))
			.WithMessage($"The API only works with the following symbols: {string.Join(", ", Shared.AcceptedSymbols)}");
		RuleFor(x => x.NumberOfDataPoints).GreaterThan(0);
		RuleFor(x => x.TimePeriod).Must(period => Shared.AcceptedTimePeriods.Contains(period))
			.WithMessage($"The API only works with the following time periods: {string.Join(", ", Shared.AcceptedTimePeriods)}");
		RuleFor(x => x.StartDateTime).LessThanOrEqualTo(DateTime.UtcNow)
			.WithMessage("The date should not be in the future");
	}
}
