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
            node.SendMessage(name);
        }

        public void Handle()
        {
            var requests = Node.ReceiveMessage();

            foreach(var request in requests)
            {
                if (request == RequestType.Message.ToString())
                {
                    Console.WriteLine(request);
                }
                else if (request == RequestType.Input.ToString())
                {
                    Console.WriteLine(request);

                    var input = Console.ReadLine() ?? "";
                    Node.SendMessage(input);
                }
                else if (request == RequestType.Choice.ToString())
                {
                    Console.WriteLine(request);

                    var input = Console.ReadLine() ?? "";
                    Node.SendMessage(input);
                }
            }
        }
    }
}