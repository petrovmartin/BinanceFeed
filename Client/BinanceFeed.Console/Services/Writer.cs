namespace BinanceFeed.Console;

public class Writer : IWriter
{
	public void Send(string message)
	{
		System.Console.WriteLine(message);
	}
}

