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
            var requests = new Stack<string>(Node.ReceiveMessage());

            while (requests.Count > 0)
            {
                var request = requests.Pop();

                if (request == RequestType.Message.ToString())
                {
                    Console.WriteLine(requests.Pop());
                }
                else if (request == RequestType.Input.ToString())
                {
                    Console.WriteLine(requests.Pop());

                    var input = Console.ReadLine() ?? "";
                    Node.SendMessage(input);
                }
                else if (request == RequestType.Choice.ToString())
                {
                    Console.WriteLine(requests.Pop());

                    var input = Console.ReadLine() ?? "";
                    Node.SendMessage(input);
                }
            }
        }
    }
}