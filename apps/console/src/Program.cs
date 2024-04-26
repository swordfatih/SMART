using Interface;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Quel est votre pseudo ?");
        var name = Console.ReadLine() ?? Guid.NewGuid().ToString();
        var client = await Client.Create(name);   
    }
}