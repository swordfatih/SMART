using Interface;
using Network;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // dotnet run --project=console <ip> <port> <name> 
        // ex: dotnet run --project=console 192.168.1.39 11000 fatih
        var node = await Node.Create(args[0], int.Parse(args[1]), NodeType.Connect);
        var client = await Client.Create(node, args[2]);

        while (true)
        {
            var message = Console.ReadLine();

            if (message == null)
            {
                break;
            }

            await client.Node.SendMessage(message);
        }
    }
}