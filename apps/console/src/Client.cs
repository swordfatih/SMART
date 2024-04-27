using Network;

namespace Interface
{
    public class Client
    {
        public string Name { get; }
        public Node Node { get; }

        private Client(Node node, string name)
        {
            Name = name;
            Node = node;
        }

        public static async Task<Client> Create(Node node, string name)
        {
            await node.SendMessage(name);
            return new Client(node, name);
        }
    }
}