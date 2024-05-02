using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Network;


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
            var packets = Node.Receive();

            foreach (var packet in packets)
            {
                if (packet.Request == RequestType.Message)
                {
                    // Update the input field text with the received message
                    Scene scene = SceneManager.GetActiveScene();
                    var roots = scene.GetRootGameObjects();
                    var inputField = roots[2].GetComponentInChildren<InputField>();
                    if (inputField == null)
                    {
                        Debug.LogError("Input field not found.");
                        return;
                    }
                    inputField.text = packet.Content[0];
                }
                else if (packet.Request == RequestType.Input)
                {
                    // Update the input field text with the received prompt
                    // inputField.text = packet.Content[0];

                    // For handling input, you may need to implement Unity UI events
                    // For example, you can attach this script to a button to send the input
                }
                else if (packet.Request == RequestType.Choice)
                {
                    // Update the input field text with the received choices
                    // inputField.text = packet.Content[0];
                }
            }
        }
    }
}