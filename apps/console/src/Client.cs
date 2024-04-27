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
            var request = Node.ReceiveMessage();

            if (request[0] == RequestType.Message.ToString())
            {
                Console.WriteLine(request[1]);
            }
            else if (request[0] == RequestType.Input.ToString())
            {
                Console.WriteLine(request[1]);

                var input = Console.ReadLine() ?? "";
                Node.SendMessage(input);
            }
            else if (request[0] == RequestType.Choice.ToString())
            {
                Console.WriteLine(request[1]);

                var input = Console.ReadLine() ?? "";
                Node.SendMessage(input);
            }
        }
    }
}