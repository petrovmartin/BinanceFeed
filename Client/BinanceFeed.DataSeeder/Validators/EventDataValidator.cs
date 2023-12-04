namespace BinanceFeed.DataSeeder;

internal static class EventDataValidator
{
    public static void ThrowIfNull(this string input)
    {
        if (input is null) throw new ArgumentNullException($"{1}: Event data format is unrecognized.", nameof(EventDataValidator));
    }
}
