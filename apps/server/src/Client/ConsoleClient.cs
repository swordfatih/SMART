using Game;

namespace Interface
{
    public class ConsoleClient(string name) : Client(name)
    {
        public override int AskChoice(Question question)
        {
            int choice;

            do
            {
                Console.WriteLine("[" + Name + "] " + question);
            }
            while (!int.TryParse(Console.ReadLine(), out choice));

            return choice;
        }

        public override string AskInput(string instruction)
        {
            Console.WriteLine("[" + Name + "] " + instruction);
            return Console.ReadLine() ?? "";
        }

        public override void SendMessage(string message)
        {
            Console.WriteLine("[" + Name + "] " + message);
        }

        public override void Notify(BoardData value)
        {
            Console.WriteLine($"------ Données pour le jour {value.Day} ------");
            Console.WriteLine("Joueurs en vie (gauche à droite, circulaire): ");

            for (var i = 0; i < value.Names.Count; ++i)
            {
                if (i != 0)
                {
                    Console.Write(" -> ");
                }

                Console.Write(value.Names[i]);
            }

            Console.WriteLine(Environment.NewLine);

        }

        public override void Notify(PlayerData value)
        {
            Console.WriteLine($"------ Données pour le joueur {value.Player.Client.Name} ({value.Player.Role}) ------");

            Console.WriteLine(value.HasGuard ? "You have the guard" : "You don't have the guard");

            if (value.Player?.Items.Count > 0)
            {
                Console.WriteLine("Vous objets: ");
                value.Player?.Items.ForEach(Console.WriteLine);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}