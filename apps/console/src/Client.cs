using System;
using Network;
using Game;
using Newtonsoft.Json;

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
                if (packet.Request == RequestType.ServerMessage)
                {
                    Console.WriteLine($"[Server] {packet.Content[0]}");
                }
                else if (packet.Request == RequestType.BoardMessage)
                {
                    Console.WriteLine($"[Board] {packet.Content[0]}");
                }
                else if (packet.Request == RequestType.PlayerMessage)
                {
                    Console.WriteLine($"[Message from {packet.Content[0]}] {packet.Content[1]}");
                }
                else if (packet.Request == RequestType.ChoiceAnswer)
                {
                    var name = packet.Content[1];
                    var choice = JsonConvert.DeserializeObject<Choice>(packet.Content[2], new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                    var answer = choice?.Answers[Convert.ToInt32(packet.Content[3])];
                    Console.WriteLine($"[Answer from {name} to {choice?.Value}] {answer}");
                }
                else if (packet.Request == RequestType.ProgressionAnswer)
                {
                    var name = packet.Content[1];
                    var choice = JsonConvert.DeserializeObject<Choice>(packet.Content[2], new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                    var answer = choice?.Answers[Convert.ToInt32(packet.Content[3])];

                    if (answer == 0)
                    {
                        var progression = packet.Content[4];
                        Console.WriteLine($"[Answer from {name} to {choice?.Value}] Progression: {progression}");
                    }
                    else
                    {
                        Console.WriteLine($"[Answer from {name} to {choice?.Value}] Refused to share progression");
                    }
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
                    var value = JsonConvert.DeserializeObject<Choice>(data, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

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
                    var value = JsonConvert.DeserializeObject<BoardData>(data, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

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
                    var value = JsonConvert.DeserializeObject<PlayerData>(data, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });

                    if (value != null)
                    {
                        Console.WriteLine($"------ Data for player {value.Player.Client.Name} ({value.Player.Role.ToString()}) ------");

                        if (value.Player.Status == Status.Dead)
                        {
                            Console.WriteLine("You are dead");
                        }
                        else
                        {
                            Console.WriteLine(value.HasGuard ? "You have the guard" : "You don't have the guard");

                            if (value.Player.Items.Count > 0)
                            {
                                Console.WriteLine("You have the following items: ");
                                foreach (var item in value.Player.Items)
                                {
                                    Console.WriteLine(item.Name);
                                }
                            }
                        }
                    }
                }
                else if (packet.Request == RequestType.Start)
                {
                    Console.WriteLine("The game is starting");
                }
                else if (packet.Request == RequestType.End)
                {
                    var team = packet.Content[0];
                    var winner = team == "null" ? "there is no winner" : $"the winners are the {team} team";

                    Console.WriteLine($"The game is over, {winner}");
                }
                else if (packet.Request == RequestType.NotifyServer)
                {
                    var data = JsonConvert.DeserializeObject<ServerData>(packet.Content[0], new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                    if (data != null)
                    {
                        Console.WriteLine("Connected players: ");
                        foreach (var client in data.Clients)
                        {
                            Console.WriteLine(client);
                        }

                        Console.WriteLine("Bots: ");
                        foreach (var bot in data.Bots)
                        {
                            Console.WriteLine(bot);
                        }

                        Console.WriteLine(data.Running ? "The game is running" : "The game is not running");
                    }
                }
            }
        }
    }
}