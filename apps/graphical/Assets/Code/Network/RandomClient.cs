using System;
using System.Diagnostics;
using Game;

namespace Interface
{
    public class RandomClient : Client
    {
        public RandomClient(string name) : base(name)
        {

        }

        public RandomClient() : base(Guid.NewGuid().ToString())
        {

        }
        
        public override int AskChoice(Question question)
        {
            Debug.WriteLine("Random: " + question.ToString());
            return new Random().Next(0, question.Answers.Count);
        }

        public override string AskInput(string instruction)
        {
            return "Random message";
        }

        public override void SendMessage(string message)
        {
            Debug.WriteLine("Random: " + message);
        }

        public override void Notify(BoardData value)
        {

        }

        public override void Notify(PlayerData value)
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}