using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Game;
using Network;
using Newtonsoft.Json;

namespace Interface
{
    public class Server
    {
        public TcpListener Listener { get; }
        public ConcurrentDictionary<TcpClient, NetworkClient?> Clients { get; }
        public List<Client> Bots { get; }
        public bool Running { get; set; } = true;
        public Board? Board { get; set; }

        public Server(string host, int port)
        {
            Listener = new TcpListener(IPAddress.Parse(host), port);
            Clients = new ConcurrentDictionary<TcpClient, NetworkClient?>();
            Bots = new();
        }

        public void Listen()
        {
            Listener.Start();

            while (Running)
            {
                var socket = Listener.AcceptTcpClient();
                Clients.TryAdd(socket, null);
            }

            Listener.Stop();
        }

        public void Receive()
        {
            while (Running)
            {
                foreach (var socket in Clients.Keys)
                {
                    if (socket.Client.Poll(0, SelectMode.SelectRead))
                    {
                        if (Clients.TryGetValue(socket, out var client) && client != null)
                        {
                            HandleAuthenticated(client);
                        }
                        else
                        {
                            HandleUnauthenticated(socket);
                        }
                    }
                }
            }
        }

        public void HandleUnauthenticated(TcpClient socket)
        {
            var node = new Node(socket);

            if (!node.Connected())
            {
                Clients.TryRemove(socket, out _);
                return;
            }

            var packets = node.Receive();

            foreach (var packet in packets)
            {
                if (packet.Request == RequestType.Connect)
                {
                    var name = packet.Content[0];

                    if (Clients.Values.Any(client => client is not null && client.Name == name && client is NetworkClient))
                    {
                        node.Send(RequestType.Error, "Name already taken.");
                        return;
                    }

                    var client = new NetworkClient(packet.Content[0], node);
                    Clients.TryUpdate(socket, client, null);
                    ReplaceClient(client.Name, client);
                    Notify();

                    client.Node.Send(new Packet(Board != null ? RequestType.Start : RequestType.Connect));
                }
            }
        }

        public void HandleAuthenticated(NetworkClient client)
        {
            if (!client.Node.Connected())
            {
                Clients.TryRemove(client.Node.Client, out _);
                ReplaceClient(client.Name, new RandomClient(client.Name));
                Notify();
                return;
            }

            var packets = client.Node.Receive();

            foreach (var packet in packets)
            {
                switch (packet.Request)
                {
                    case RequestType.Start:
                        if (Board != null)
                        {
                            client.Node.Send(new Packet(RequestType.Error, new[] { "Game already started." }));
                            break;
                        }

                        Task.Run(Start);
                        break;
                    case RequestType.NotifyServer:
                        Notify();
                        break;
                    default:
                        client.Node.Packets.Enqueue(packet);
                        break;
                }
            }
        }

        public void Start()
        {
            var clients = new List<Client>();

            Clients.Values.ToList().ForEach(client =>
            {
                if (client != null)
                {
                    clients.Add(client);
                }
            });

            Bots.ForEach(bot => clients.Add(bot));

            var file = File.Open("logs.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            Board = new Board(file);

            Board.Init(clients);

            Notify();
            Broadcast(new Packet(RequestType.Start));
            Board.Run();

            var winner = Board.GetWinner();
            Broadcast(new Packet(RequestType.End, new string[] { winner.ToString() ?? "null" }));

            Board = null;
        }

        public void Broadcast(Packet packet)
        {
            foreach (var client in Clients.Values)
            {
                if (client is NetworkClient network)
                {
                    network.Node.Send(packet);
                }
            }
        }

        public void Broadcast(string message)
        {
            Broadcast(new Packet(RequestType.ServerMessage, new[] { message }));
        }

        private void ReplaceClient(string name, Client source)
        {
            if (Board != null)
            {
                if (Board.Players.FindByName(name) is Player player)
                {
                    if (player.Client is NetworkClient networkClient)
                    {
                        networkClient.Node.Packets.Enqueue(new Packet(RequestType.Disconnect, new[] { "You have been disconnected." }));
                    }

                    Board.Observers.Remove(player.Client);
                    player.SetClient(source);
                    Board.Subscribe(player.Client);
                    Board.Notify(player.Client);
                }
            }
        }

        private void Notify()
        {
            Broadcast(new Packet(RequestType.NotifyServer, new[] {
                JsonConvert.SerializeObject(
                    new ServerData(
                        Clients.Where(x => x.Value != null).Select(x => x.Value?.Name ?? ""),
                        Bots.Select(x => x.Name),
                        Board != null
                    ),
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    }
                )
            }));
        }
    }
}