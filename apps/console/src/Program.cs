using Board;
using Client;

internal class Program
{
    private static void Main(string[] args)
    {
        List<IClient> clients = [
            new ConsoleClient("fatih"),
            new ConsoleClient("oggy"),
            new RandomClient(),
        ];

        var game = new Game(clients);
        game.Init();
        Console.WriteLine(game);
        game.Run();
    }
}