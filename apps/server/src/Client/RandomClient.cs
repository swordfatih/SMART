using Board;

namespace Interface
{
    public class RandomClient() : IClient
    {
        public string Name { get; } = Guid.NewGuid().ToString();

        public int AskChoice(Question question)
        {
            return new Random().Next(0, question.Answers.Count);
        }

        public string AskInput(string instruction)
        {
            return "Random message";
        }

        public void SendMessage(string message)
        {
            
        }

        public override string ToString()
        {   
            return Name;
        }
    }
}