namespace Player
{
    public abstract class Instruction(string value)
    {
        public string Value { get; init; } = value;

        public override string ToString()
        {
            return Value;
        }
    }

    public class Question(string value, List<Instruction> answers) : Instruction(value)
    {
        public List<Instruction> Answers { get; init; } = answers;

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

    public class Answer(string value, Action action) : Instruction(value)
    {
        public Action Action { get; init; } = action;
    }
}