using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

                    if (Clients.Values.Any(client => client?.Name == name))
                    {
                        node.Send(RequestType.Error, "Name already taken.");
                        return;
                    }

                    var client = new NetworkClient(packet.Content[0], node);
                    Clients.TryUpdate(socket, client, null);

                    Console.WriteLine($"{name} connected.");
                }
            }
        }

        public void HandleAuthenticated(NetworkClient client)
        {
            if (!client.Node.Connected())
            {
                Clients.TryRemove(client.Node.Client, out _);
                Console.WriteLine($"{client.Name} disconnected.");
                return;
            }

            var packets = client.Node.Receive();

            foreach (var packet in packets)
            {
                switch (packet.Request)
                {
                    case RequestType.Start:
                        Broadcast(RequestType.Message, "Game starting.");
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

            var board = new Board(clients);

            board.Init();

            clients.ForEach(board.Subscribe);
            clients.ForEach(client => board.Players.Find(x => x.Client == client)?.Subscribe(client));

            Console.WriteLine(board);
            board.Run();

            Broadcast(RequestType.Message, "Game over.");
        }

        public void Broadcast(RequestType request, params string[] content)
        {
            var packet = new Packet(request, content);
            foreach (var client in Clients.Values)
            {
                client?.Node.Send(packet);
            }
        }
    }
}