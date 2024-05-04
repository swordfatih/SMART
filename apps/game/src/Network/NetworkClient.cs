using Network;
using Game;
using System;
using Newtonsoft.Json;

namespace Interface
{
    public class NetworkClient : Client
    {
        [JsonIgnore]
        public Node Node { get; }

        public NetworkClient(string name, Node node) : base(name)
        {
            Node = node;
        }

        public override int AskChoice(Question question)
        {
            Node.Send(RequestType.Choice, JsonConvert.SerializeObject(question, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            }));

            while (true)
            {
                Node.Packets.TryDequeue(out var packet);

                if (packet?.Request == RequestType.Choice)
                {
                    return Convert.ToInt32(packet.Content[0]);
                }
                else if (packet?.Request == RequestType.Disconnect)
                {
                    throw new Exception("Disconnected from server");
                }
            }
        }

        public override string AskInput(string instruction)
        {
            Node.Send(RequestType.Input, instruction);

            while (true)
            {
                Node.Packets.TryDequeue(out var packet);

                if (packet?.Request == RequestType.Input)
                {
                    return packet.Content[0];
                }
                else if (packet?.Request == RequestType.Disconnect)
                {
                    throw new Exception("Disconnected from server");
                }
            }
        }

        public override void SendPlayerMessage(string origin, string message)
        {
            Node.Send(RequestType.PlayerMessage, message);
        }

        public override void SendBoardMessage(string message)
        {
            Node.Send(RequestType.BoardMessage, message);
        }

        public override void SendChoiceAnswer(string origin, Question question, int choice)
        {
            Node.Send(RequestType.ChoiceAnswer, new string[]{origin, JsonConvert.SerializeObject(question), choice.ToString()});
        }

        public override void Notify(BoardData value)
        {
            Node.Send(RequestType.NotifyBoard, JsonConvert.SerializeObject(value));
        }

        public override void Notify(PlayerData value)
        {
            Node.Send(RequestType.NotifyPlayer, JsonConvert.SerializeObject(value));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}