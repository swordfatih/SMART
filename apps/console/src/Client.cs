using System.Net.Sockets;
using Network;

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
                    Node.Send(RequestType.Message, input);
                }
                else if (packet.Request == RequestType.Choice)
                {
                    Console.WriteLine(packet.Content[0]);

                    var input = Console.ReadLine() ?? "";
                    Node.Send(RequestType.Choice, input);
                }
            }
        }
    }
}