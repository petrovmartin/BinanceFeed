namespace BinanceFeed.Console;

public class Reader : IReader
{
	public string Read()
	{
		var input = System.Console.ReadLine() ?? System.Console.ReadLine();
		return input;
	}
}
