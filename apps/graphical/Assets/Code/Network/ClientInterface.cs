using UnityEngine;
using UnityEngine.UI;
using Network;
using System.Net.Sockets;
using Game;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

namespace Interface
{
    public class ClientInterface
    {
        public string Name { get; }
        public Node Node { get; }

        public ClientInterface(Node node, string name)
        {
            Name = name;
            Node = node;

            node.Send(RequestType.Connect, name);
        }

        public void Handle()
        {
            if (Node.Client.Client.Poll(0, SelectMode.SelectRead))
            {
                if (!Node.Connected())
                {
                    Debug.Log("Connection lost.");
                    GameManager.Instance.Client = null;
                    SceneManager.LoadScene("S_Menu");
                    return;
                }
                else
                {
                    var packets = Node.Receive();

                    foreach (var packet in packets)
                    {
                        if (packet.Request == RequestType.PlayerMessage)
                        {
                            var origin = packet.Content[0];
                            var message = packet.Content[1];
                            GameManager.Instance.Notify(new Message(origin, message));
                        }
                        else if (packet.Request == RequestType.ServerMessage)
                        {
                            var message = packet.Content[0];
                            GameManager.Instance.Notify(new Message("Server", message));
                        }
                        else if (packet.Request == RequestType.BoardMessage)
                        {
                            var message = packet.Content[0];
                            GameManager.Instance.Notify(new Message("Board", message));
                        }
                        else if (packet.Request == RequestType.Connect)
                        {
                            SceneManager.LoadScene("S_Lobby");
                        }
                        else if (packet.Request == RequestType.End)
                        {
                            var team = packet.Content[0];
                            var winner = team == "null" ? "there is no winner" : $"the winners are the {team} team";

                            SceneManager.LoadScene("S_Lobby");
                            GameManager.Instance.Notify(new Message("Board", "The game is over, " + winner));
                        }
                        else if (packet.Request == RequestType.Start)
                        {
                            Debug.Log("The game is starting...");
                            SceneManager.LoadScene("S_Prison_Inside");
                            SceneManager.LoadScene("S_HUD", LoadSceneMode.Additive);
                        }
                        else if (packet.Request == RequestType.Error)
                        {
                            var message = packet.Content[0];
                            Debug.Log($"Error: {message}");

                            GameManager.Instance.Notify(new Message("Error", message));
                        }
                        else if (packet.Request == RequestType.Choice)
                        {
                            var value = JsonConvert.DeserializeObject<Choice>(packet.Content[0], new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });

                            GameManager.Instance.Notify(value);
                        }
                        else if (packet.Request == RequestType.NotifyServer)
                        {
                            var data = JsonConvert.DeserializeObject<ServerData>(packet.Content[0], new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });

                            GameManager.Instance.Notify(data);
                        }
                        else if (packet.Request == RequestType.NotifyPlayer)
                        {
                            var data = JsonConvert.DeserializeObject<PlayerData>(packet.Content[0], new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });

                            if (data.Player.States.Count != 0)
                            {
                                if (data.Player.States.Peek() is ConfinedState)
                                {
                                    SceneManager.LoadScene("S_Isolement");
                                }
                                else if (data.Player.States.Peek() is GuardState || data.Player.States.Peek() is SafeState)
                                {
                                    SceneManager.LoadScene("S_Prison_Inside");
                                }
                                else if (data.Player.States.Peek() is ShowerState)
                                {
                                    SceneManager.LoadScene("S_Shower");
                                }

                                SceneManager.LoadScene("S_HUD", LoadSceneMode.Additive);
                            }

                            GameManager.Instance.Notify(data);
                        }
                        else if (packet.Request == RequestType.NotifyBoard)
                        {
                            var data = JsonConvert.DeserializeObject<BoardData>(packet.Content[0], new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });

                            GameManager.Instance.Notify(data);
                        }
                        else if (packet.Request == RequestType.Input)
                        {
                            var instruction = packet.Content[0];
                            GameManager.Instance.Notify(new UserInput(instruction));
                        }
                    }
                }
            }
        }
    }
}