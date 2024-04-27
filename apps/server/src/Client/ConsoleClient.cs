using Game;

namespace Interface
{
    public class ConsoleClient(string name) : IClient
    {
        public string Name { get; } = name;

        public int AskChoice(Question question)
        {
            int choice;

            do
            {
                Console.WriteLine("[" + Name + "] " + question);
            }
            while (!int.TryParse(Console.ReadLine(), out choice));

            return choice;
        }

        public string AskInput(string instruction)
        {
            Console.WriteLine("[" + Name + "] " + instruction);
            return Console.ReadLine() ?? "";
        }

        public void SendMessage(string message)
        {
            Console.WriteLine("[" + Name + "] " + message);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}