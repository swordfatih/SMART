using System.Net.Sockets;
using Network;

namespace Interface
{
    public class Server(Node node)
    {
        public Node Node { get; } = node;
        public Dictionary<Socket, NetworkClient> Clients { get; } = [];

        public void Accept()
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
                        var client = socket.Accept();
                        var handler = new Node(client);
                        var name = handler.ReceiveMessage()[0];

                        Clients[client] = new NetworkClient(name, handler);
                    }
                    else
                    {
                        var client = Clients[socket];

                        if (client.Node.Connected())
                        {
                            var request = client.Node.ReceiveMessage();
                            
                            if(request[0] == RequestType.Start.ToString())
                            {
                                break;
                            }
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