using Network;
using Game;
using System;
using System.Text.Json;

namespace Interface
{
    public class NetworkClient : Client
    {
        public Node Node { get; }

        public NetworkClient(string name, Node node) : base(name)
        {
            Node = node;
        }

        public override int AskChoice(Question question)
        {
            Node.Send(RequestType.Choice, JsonSerializer.Serialize(question));

            while (true)
            {
                if (Node.Packets.TryDequeue(out var packet) && packet.Request == RequestType.Choice)
                {
                    return Convert.ToInt32(packet.Content[0]);
                }
            }
        }

        public override string AskInput(string instruction)
        {
            Node.Send(RequestType.Input, instruction);

            while (true)
            {
                if (Node.Packets.TryDequeue(out var packet) && packet.Request == RequestType.Input)
                {
                    return packet.Content[0];
                }
            }
        }

        public override void SendMessage(string message)
        {
            Node.Send(RequestType.Message, message);
        }

        public override void Notify(BoardData value)
        {
            Node.Send(RequestType.NotifyBoard, JsonSerializer.Serialize(value));
        }

        public override void Notify(PlayerData value)
        {
            Node.Send(RequestType.NotifyPlayer, JsonSerializer.Serialize(value));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}