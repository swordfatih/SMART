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
                    GameManager.Instance.Disconnect();
                    return;
                }
                else
                {
                    var packets = Node.Receive();

                    Debug.Log("Received " + packets.Count + " packets.");

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
                        else if (packet.Request == RequestType.Input)
                        {
                            var instruction = packet.Content[0];

                            var controls = GameObject.Find("Controls");
                            foreach (Transform child in controls.transform)
                            {
                                GameObject.Destroy(child.gameObject);
                            }

                            var text = new GameObject("Text", typeof(Text));
                            text.transform.SetParent(controls.transform);
                            text.GetComponent<Text>().text = instruction;
                            text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
                            text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                            text.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);

                            var input = new GameObject("Input", typeof(InputField));
                            input.transform.SetParent(controls.transform);

                            input.GetComponent<InputField>().onEndEdit.AddListener((string value) =>
                            {
                                Node.Send(RequestType.Input, value);
                            });

                            var textRect = text.GetComponent<RectTransform>();
                            var inputRect = input.GetComponent<RectTransform>();
                            inputRect.position = new Vector3(0, 0, 0);
                            input.transform.localPosition = new Vector3(0, -50, 0);
                        }
                        else if (packet.Request == RequestType.Choice)
                        {
                            var value = JsonConvert.DeserializeObject<Choice>(packet.Content[0], new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });

                            GameManager.Instance.Notify(value);
                        }
                        else if (packet.Request == RequestType.End)
                        {
                            var team = packet.Content[0];
                            var winner = team == "null" ? "there is no winner" : $"the winners are the {team} team";

                            Debug.Log($"The game is over, {winner}");
                            GameManager.Instance.Disconnect();
                        }
                        else if (packet.Request == RequestType.Error)
                        {
                            var message = packet.Content[0];
                            Debug.Log($"Error: {message}");
                            GameManager.Instance.Notify(new Message("Error", message));
                        }
                        else if (packet.Request == RequestType.NotifyServer)
                        {
                            var data = JsonConvert.DeserializeObject<ServerData>(packet.Content[0], new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });

                            GameManager.Instance.Notify(data);
                        }
                    }

                    Debug.Log("End packets...");
                }
            }
        }
    }
}