using System.Net.Sockets;
using System.Text;
using Interface;
using Network;

internal class Program
{
    private static void Main(string[] args)
    {
        // dotnet run --project=console <ip> <port> <name> 
        // ex: dotnet run --project=console 192.168.1.39 11000 fatih
        var node = new Node(args[0], int.Parse(args[1]));
        var client = new Client(node, args[2]);

        Console.WriteLine("Press 'start' to start the game.");

        while (true)
        {
            if (client.Node.Client.Client.Poll(0, SelectMode.SelectRead))
            {
                if (!client.Node.Connected())
                {
                    Console.WriteLine("Connection lost.");
                    break;
                }
                else
                {
                    client.Handle();
                }
            }

            if (Console.KeyAvailable)
            {
                string input = Console.ReadLine() ?? "";

                if (input.Contains("start"))
                {
                    client.Node.Send(RequestType.Start);
                }
            }

            Thread.Sleep(100);
        }
    }
}