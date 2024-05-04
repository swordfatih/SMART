using UnityEngine;
using UnityEngine.UI;
using Network;
using System.Net.Sockets;
using Game;
using Newtonsoft.Json;

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
            var controls = GameObject.Find("Controls");

            if (Node.Client.Client.Poll(0, SelectMode.SelectRead))
            {
                if (!Node.Connected())
                {
                    Debug.Log("Connection lost.");
                    return;
                }
                else
                {
                    var packets = Node.Receive();

                    Debug.Log("Received " + packets.Count + " packets.");

                    foreach (var packet in packets)
                    {
                        Debug.Log("Handling packet...");
                        if (packet.Request == RequestType.PlayerMessage)
                        {
                            Debug.Log("Message packet...");
                            var message = packet.Content[0];

                            foreach (Transform child in controls.transform)
                            {
                                GameObject.Destroy(child.gameObject);
                            }

                            var text = new GameObject("Text", typeof(Text));
                            text.transform.SetParent(controls.transform);
                            text.GetComponent<Text>().text = message;
                            text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
                            text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                            text.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
                            text.transform.localPosition = Vector3.zero;
                        }
                        else if (packet.Request == RequestType.Input)
                        {
                            Debug.Log("Input packet...");
                            var instruction = packet.Content[0];

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

                            Debug.Log("End input packet...");
                        }
                        else if (packet.Request == RequestType.Choice)
                        {
                            Debug.Log("Choice packet...");
                            var data = packet.Content[0];
                            var value = JsonConvert.DeserializeObject<Question>(data, new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });

                            GameObject popMessageObject = GameObject.Find("message");

                            // Récupérer le composant popMessage attaché à l'objet
                            popMessage popMessageScript = popMessageObject.GetComponent<popMessage>();

                            // Vérifier si le composant a été trouvé
                            if (popMessageScript != null)
                            {
                                popMessageScript.SetMessage(value);
                            }

                            Debug.Log("End choice packet...");
                        }
                        else if (packet.Request == RequestType.End)
                        {
                            var team = packet.Content[0];
                            var winner = team == "null" ? "there is no winner" : $"the winners are the {team} team";

                            Debug.Log($"The game is over, {winner}");
                        }

                        Debug.Log("End choice packet...");
                    }

                    Debug.Log("End packets...");
                }
            }
        }
    }
}