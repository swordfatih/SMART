using Game;
using Interface;
using Network;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // dotnet run --project=server <ip> <port>
        // ex: dotnet run --project=server 192.168.1.39 11000
        var node = await Node.Create(args[0], int.Parse(args[1]), NodeType.Bind);
        var server = new Server(node);
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