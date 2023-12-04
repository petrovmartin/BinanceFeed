using FluentValidation;

namespace BinanceFeed.API;

public class Get24hAvgRequestValidator : AbstractValidator<Get24hAvgRequest>
{
	public Get24hAvgRequestValidator()
	{
		RuleFor(x => x.Symbol)
			.NotEmpty()
			.Must(symbol => Shared.AcceptedSymbols.Contains(symbol.ToLowerInvariant()))
			.WithMessage($"The API only works with the following symbols: {string.Join(", ", Shared.AcceptedSymbols)}");
	}
}