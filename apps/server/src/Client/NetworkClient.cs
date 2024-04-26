using System.Net;
using System.Net.Sockets;
using Board;

namespace Interface
{
    public class NetworkClient(string name, Socket socket) : IClient
    {
        public string Name { get; } = name;
        public Socket Socket { get; } = socket;

        public int AskChoice(Question question)
        {
            throw new NotImplementedException();
        }

        public string AskInput(string instruction)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string message)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}