using System;
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
            return new Random().Next(0, question.Answers.Count);
        }

        public override string AskInput(string instruction)
        {
            return "";
        }

        public override void SendPlayerMessage(string origin, string message)
        {
        }

        public override void SendBoardMessage(string message)
        {
        }

        public override void SendChoiceAnswer(int position, Question question, int choice)
        {
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