namespace Board
{
    public class Question(string value, List<string> answers)
    {
        public string Value { get; init; } = value;
        public List<string> Answers { get; init; } = answers;

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