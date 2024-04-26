using Board;
using Client;

internal class Program
{
    private static void Main(string[] args)
    {
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