using System.Net.Sockets;
using System.Text;
using Interface;
using Network;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // dotnet run --project=console <ip> <port> <name> 
        // ex: dotnet run --project=console 192.168.1.39 11000 fatih
        var node = await Node.Create(args[0], int.Parse(args[1]), NodeType.Connect);
        var client = new Client(node, args[2]);

        Console.WriteLine("Press 'start' to start the game.");

        while (true)
        {
            if (client.Node.Socket.Poll(0, SelectMode.SelectRead))
            {
                if (!client.Node.Connected())
                {
                    Console.WriteLine("Server closed.");
                    break;
                }

                client.Handle();
            }

            if (Console.KeyAvailable)
            {
                string input = Console.ReadLine() ?? "";

                if (input.Contains("start"))
                {
                    client.Node.SendMessage(RequestType.Start.ToString());
                }
            }

            Thread.Sleep(100);
        }
    }
}