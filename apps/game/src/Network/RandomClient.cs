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

        public override int SendChoice(Choice choice)
        {
            return new Random().Next(0, choice.Answers.Count);
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

        public override void SendChoiceAnswer(int position, string name, Choice choice, int answer)
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