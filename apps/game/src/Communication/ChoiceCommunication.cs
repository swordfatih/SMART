namespace Game
{
    public class ChoiceCommunication : Communication
    {
        public readonly Question Question;

        public ChoiceCommunication(Player origin, Direction direction, Question question) : base(origin, direction)
        {
            Question = question;
        }
    }
}