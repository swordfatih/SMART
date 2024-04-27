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
            Node.SendMessage(RequestType.Choice.ToString());
            Node.SendMessage(question.ToString());
            var choice = Node.ReceiveMessage();
            return Convert.ToInt32(choice[0]);
        }

        public string AskInput(string instruction)
        {
            Node.SendMessage(RequestType.Input.ToString() + Node.EOM + instruction);
            return Node.ReceiveMessage()[0];
        }

        public void SendMessage(string message)
        { 
            Node.SendMessage(RequestType.Message.ToString() + Node.EOM + message);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}