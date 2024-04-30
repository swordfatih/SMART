using Network;
using Game;
using System;

namespace Interface
{
    public class NetworkClient : Client
    {
        public Node Node { get; init; }

        public NetworkClient(string name, Node node) : base(name)
        {
            Node = node;
        }

        public override int AskChoice(Question question)
        {
            Node.Send(RequestType.Choice, question.ToString());

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
            var output = "";
            output += $"------ Données pour le jour {value.Day} ------";
            output += "Joueurs en vie (gauche à droite, circulaire): ";

            for (var i = 0; i < value.Names.Count; ++i)
            {
                if (i != 0)
                {
                    output += " -> ";
                }

                output += value.Names[i];
            }

            output += Environment.NewLine;

            Node.Send(RequestType.Message, output);
        }

        public override void Notify(PlayerData value)
        {
            var output = "";
            output += $"------ Données pour le joueur {value.Player.Client.Name} ({value.Player.Role}) ------";
            output += Environment.NewLine;
            output += value.HasGuard ? "You have the guard" : "You don't have the guard";

            if (value.Player?.Items.Count > 0)
            {
                output += "Vous objets: ";
                value.Player?.Items.ForEach(x => output += x + Environment.NewLine);
            }

            Node.Send(RequestType.Message, output);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}