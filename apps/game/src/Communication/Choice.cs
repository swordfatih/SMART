using System.Collections.Generic;

namespace Game
{
    public class Choice
    {
        public string Value { get; }
        public List<string> Answers { get; }
        public int Origin { get; }

        public Choice(string value, List<string> answers, int origin = -1)
        {
            Value = value;
            Answers = answers;
            Origin = origin;
        }

        public override string ToString()
        {
            string output = Value;

            for (var i = 0; i < Answers.Count; ++i)
            {
                output += "\t" + i + ". " + Answers[i];
            }

            return output;
        }
    }
}