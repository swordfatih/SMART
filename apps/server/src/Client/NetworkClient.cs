using Network;
using Game;

namespace Interface
{
    public class NetworkClient(string name, Node node) : Client(name)
    {
        public Node Node { get; } = node;

        public override int AskChoice(Question question)
        {
            Node.SendMessage(RequestType.Choice.ToString() + Node.EOM + question.ToString());
            var choice = Node.ReceiveMessage();
            return Convert.ToInt32(choice[0]);
        }

        public override string AskInput(string instruction)
        {
            Node.SendMessage(RequestType.Input.ToString() + Node.EOM + instruction);
            return Node.ReceiveMessage()[0];
        }

        public override void SendMessage(string message)
        {
            Node.SendMessage(RequestType.Message.ToString() + Node.EOM + message);
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

            Node.SendMessage(RequestType.Message + Node.EOM + output);
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

            Node.SendMessage(RequestType.Message + Node.EOM + output);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}