using System.Net;
using Game;
using Interface;
using Network;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // dotnet run --project=server <port>
        // ex: dotnet run --project=server 11000
        
        var host = (await Node.GetLocalAddress()).ToString();
        var port = int.Parse(args[0]);

        var node = await Node.Create(host, port, NodeType.Bind);
        var server = new Server(node);

        Console.WriteLine("Starting server on " + host + " (" + port + ")");
        
        server.Accept();

        Console.WriteLine("Game is starting.");

        var clients = new List<IClient>(server.Clients.Select(x => x.Value));
        var board = new Board(clients);

        board.Init();
        board.GuardPosition = 0;

        Console.WriteLine(board);
        board.Run();
    }
}