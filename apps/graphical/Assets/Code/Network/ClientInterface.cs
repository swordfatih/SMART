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
                        if (packet.Request == RequestType.Message)
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
                                Node.Send(RequestType.Message, value);
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

                            foreach (Transform child in controls.transform)
                            {
                                GameObject.Destroy(child.gameObject);
                            }

                            var instruction = new GameObject("Instruction", typeof(Text));
                            instruction.transform.SetParent(controls.transform);
                            instruction.GetComponent<Text>().text = value.Value;
                            instruction.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
                            var instructionRect = instruction.GetComponent<RectTransform>();
                            instructionRect.position = new Vector3(0, 0, 0);
                            instruction.transform.localPosition = Vector3.zero;

                            for (var i = 0; i < value.Answers.Count; ++i)
                            {
                                var choice = new GameObject("Choice", typeof(Button));
                                choice.transform.SetParent(controls.transform);
                                choice.GetComponent<Button>().onClick.AddListener(() =>
                                {
                                    Node.Send(RequestType.Choice, i.ToString());
                                });

                                // add text to button
                                var choiceText = new GameObject("Text", typeof(Text));
                                choiceText.transform.SetParent(choice.transform);
                                choiceText.GetComponent<Text>().text = value.Answers[i];
                                choiceText.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
                                choiceText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

                                var choiceRect = choice.AddComponent<RectTransform>();
                                choiceRect.position = new Vector3(0, 0, 0);
                                choice.transform.localPosition = new Vector3(0, -50 * i, 0);

                                // height of button 50
                                choiceRect.sizeDelta = new Vector2(200, 50);
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