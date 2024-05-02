using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Game;
using Network;

namespace Interface
{
    public class Server
    {
        public TcpListener Listener { get; }
        public ConcurrentDictionary<TcpClient, NetworkClient?> Clients { get; }
        public bool Running { get; set; } = true;
        public Board? Board { get; set; }

        public Server(string host, int port)
        {
            Listener = new TcpListener(IPAddress.Parse(host), port);
            Clients = new ConcurrentDictionary<TcpClient, NetworkClient?>();
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
                    // ReplaceClient(client.Name, client);

                    Console.WriteLine($"{name} connected.");
                    Broadcast($"{name} connected.");
                }
            }
        }

        public void HandleAuthenticated(NetworkClient client)
        {
            if (!client.Node.Connected())
            {
                Clients.TryRemove(client.Node.Client, out _);
                ReplaceClient(client.Name, new ConsoleClient(client.Name));

                Console.WriteLine($"{client.Name} disconnected.");
                Broadcast($"{client.Name} disconnected.");

                return;
            }

            var packets = client.Node.Receive();

            foreach (var packet in packets)
            {
                switch (packet.Request)
                {
                    case RequestType.Start:
                        Task.Run(Start);
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

            var file = File.Open("logs.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            Board = new Board(file);

            Board.Init(clients);

            clients.ForEach(Board.Subscribe);
            clients.ForEach(client => Board.Players.Find(x => x.Client == client)?.Subscribe(client));

            Console.WriteLine(Board);

            Broadcast("Game starting.");
            Board.Run();
            Broadcast("Game over.");
        }

        public void Broadcast(string message)
        {
            foreach (var client in Clients.Values)
            {
                client?.SendMessage(message);
            }
        }

        private void ReplaceClient(string name, Client source)
        {
            if (Board != null)
            {
                var player = Board.Players.Find(x => x.Client.Name == name);

                if (player != null)
                {
                    player.Client = source;

                    if (player.GetCurrentAction() is not null)
                    {
                        player.RestartAction();
                    }
                }
            }
        }
    }
}