using Network;
using Game;

namespace Interface
{
    public class NetworkClient(string name, Node node) : IClient
    {
        public string Name { get; } = name;
        public Node Node { get; } = node;

        public int AskChoice(Question question)
        {
            throw new NotImplementedException();
        }

        public string AskInput(string instruction)
        {
            throw new NotImplementedException();
        }

        public async void SendMessage(string message)
        {
            await Node.SendMessage("Message");
            await Node.SendMessage(message);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}