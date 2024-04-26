using Board;
using Interface;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var server = await Server.Create();
        await server.Run();

        List<IClient> clients = [
            new ConsoleClient("oussama"),
            new ConsoleClient("sarah"),
            new ConsoleClient("diland"),
            new ConsoleClient("daniel"),
        ];

        var game = new Game(clients);
        game.Init();
        game.GuardPosition = 0;

        Console.WriteLine(game);
        game.Run();
    }
}