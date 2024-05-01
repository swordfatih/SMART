using System;
using System.Text.Json;
using Network;
using Game;

namespace Interface
{
    public class Client
    {
        public string Name { get; }
        public Node Node { get; }

        public Client(Node node, string name)
        {
            Name = name;
            Node = node;

            node.Send(RequestType.Connect, name);
        }

        public void Handle()
        {
            var packets = Node.Receive();

            foreach (var packet in packets)
            {
                if (packet.Request == RequestType.Message)
                {
                    Console.WriteLine(packet.Content[0]);
                }
                else if (packet.Request == RequestType.Input)
                {
                    Console.WriteLine(packet.Content[0]);

                    var input = Console.ReadLine() ?? "";
                    Node.Send(RequestType.Input, input);
                }
                else if (packet.Request == RequestType.Choice)
                {
                    var data = packet.Content[0];
                    var value = JsonSerializer.Deserialize<Question>(data);

                    if (value != null)
                    {
                        Console.WriteLine(value.Value);
                        Console.WriteLine("Choices: ");

                        for (var i = 0; i < value.Answers.Count; ++i)
                        {
                            Console.WriteLine($"{i + 1}. {value.Answers[i]}");
                        }

                        var input = Convert.ToInt32(Console.ReadLine() ?? "1") - 1;
                        Node.Send(RequestType.Choice, input.ToString());
                    }
                }
                else if (packet.Request == RequestType.Error)
                {
                    Console.WriteLine(packet.Content[0]);
                    Environment.Exit(1);
                }
                else if (packet.Request == RequestType.NotifyBoard)
                {
                    var data = packet.Content[0];
                    var value = JsonSerializer.Deserialize<BoardData>(data);

                    if (value != null)
                    {
                        Console.WriteLine($"------ Données pour le jour {value.Day} ------");
                        Console.WriteLine("Joueurs en vie (gauche à droite, circulaire): ");

                        for (var i = 0; i < value.Names.Count; ++i)
                        {
                            if (i != 0)
                            {
                                Console.Write(" -> ");
                            }

                            Console.Write(value.Names[i]);
                        }

                        Console.WriteLine();
                    }
                }
                else if (packet.Request == RequestType.NotifyPlayer)
                {
                    var data = packet.Content[0];
                    var value = JsonSerializer.Deserialize<PlayerData>(data);

                    if (value != null)
                    {
                        Console.WriteLine($"------ Data for player {value.Player.Client.Name} ------");
                        Console.WriteLine(value.HasGuard ? "You have the guard" : "You don't have the guard");

                        if (value.Player.Items.Count > 0)
                        {
                            Console.WriteLine("You have the following items: ");
                            foreach (var item in value.Player.Items)
                            {
                                Console.WriteLine(item);
                            }
                        }
                    }
                }
            }
        }
    }
}