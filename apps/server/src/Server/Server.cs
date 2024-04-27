using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Network;

namespace Interface
{
    public class Server(Node node)
    {
        public Node Node { get; } = node;
        public Dictionary<Socket, NetworkClient> Clients { get; } = [];

        public async Task Run()
        {
            Node.Socket.Listen(100);

            while (true)
            {
                List<Socket> sockets = [Node.Socket];
                Clients.Keys.ToList().ForEach(sockets.Add);
                Socket.Select(sockets, null, null, 100);

                if (sockets.Count > 0)
                {
                    var socket = sockets.First();

                    if (socket == Node.Socket)
                    {
                        var client = await socket.AcceptAsync();
                        var handler = new Node(client);
                        var name = await handler.ReceiveMessage();

                        Clients[client] = new NetworkClient(name, handler);
                    }
                    else
                    {
                        var client = Clients[socket];

                        if (client.Node.Connected())
                        {
                            var message = await client.Node.ReceiveMessage();
                            Console.WriteLine(client.Name + ": " + message);
                        }
                        else
                        {
                            Clients.Remove(socket);
                            Console.WriteLine(client.Name + " disconnected.");
                        }
                    }
                }
            }
        }
    }
}