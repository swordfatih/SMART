using System;
using Game;

namespace Interface
{
    public class ConsoleClient : Client
    {
        public ConsoleClient(string name) : base(name)
        {

        }

        public override int SendChoice(Choice choice)
        {
            int answer;

            do
            {
                Console.WriteLine("[" + Name + "] " + choice);
            }
            while (!int.TryParse(Console.ReadLine(), out answer));

            return answer;
        }

        public override string AskInput(string instruction)
        {
            Console.WriteLine("[" + Name + "] " + instruction);
            return Console.ReadLine() ?? "";
        }

        public override void SendPlayerMessage(string origin, string message)
        {
            Console.WriteLine($"[Message from {origin}] ${message}");
        }

        public override void SendBoardMessage(string message)
        {
            Console.WriteLine("[Board] " + message);
        }

        public override void SendChoiceAnswer(int position, Choice choice, int answer)
        {
            Console.WriteLine($"[Answer from {position} to ${choice.Value}] {choice.Answers[answer]}");
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
                Console.WriteLine("Vos objets: ");
                value.Player?.Items.ForEach(x => Console.WriteLine(x.Name));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}