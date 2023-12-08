namespace BinanceFeed.DataSeeder;

internal static class EventDataValidator
{
    public static void ThrowIfNull(this string input)
    {
        if (input is null) throw new ArgumentNullException($"{nameof(EventDataValidator)}: Event data format is unrecognized.");
    }
}
