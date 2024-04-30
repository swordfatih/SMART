using System.Collections.Generic;

namespace Game
{
    public class Question
    {
        public string Value { get; }
        public List<string> Answers { get; }

        public Question(string value, List<string> answers)
        {
            Value = value;
            Answers = answers;
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