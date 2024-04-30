using System;
using System.Threading.Tasks;
using Game;
using Interface;
using Network;

internal class Program
{
    private static void Main(string[] args)
    {
        // dotnet run --project=server <port>
        // ex: dotnet run --project=server 11000
        var server = new Server(args[0], Convert.ToInt32(args[1]));

        Console.WriteLine("Starting server on " + args[0] + " (" + args[1] + ")");

        var listener = Task.Run(server.Listen);
        var receiver = Task.Run(server.Receive); 

        listener.Wait();
        receiver.Wait();
    }
}