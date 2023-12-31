﻿using FluentValidation;

namespace BinanceFeed.API;

public static class Shared
{
	public static readonly string[] AcceptedSymbols = ["ethusdt", "adausdt", "btcusdt"];
	public static readonly string[] AcceptedTimePeriods = ["1w", "1d", "30m", "5m", "1m"];

	public static IRuleBuilderOptions<T, TProperty> In<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, params TProperty[] validOptions)
	{
		string formatted;
		if (validOptions == null || validOptions.Length == 0)
		{
			throw new ArgumentException("At least one valid option is expected", nameof(validOptions));
		}
		else if (validOptions.Length == 1)
		{
			formatted = validOptions[0].ToString();
		}
		else
		{
			// format like: option1, option2 or option3
			formatted = $"{string.Join(", ", validOptions.Select(vo => vo.ToString()).ToArray(), 0, validOptions.Length - 1)} or {validOptions.Last()}";
		}

		return ruleBuilder
			.Must(validOptions.Contains)
			.WithMessage($"{{PropertyName}} must be one of these values: {formatted}");
	}
}
