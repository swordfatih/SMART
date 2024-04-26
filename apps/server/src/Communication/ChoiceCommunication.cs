namespace Board
{
    public class ChoiceCommunication(Player origin, Direction direction, Question question) : Communication(origin, direction)
    {
        public readonly Question Question = question;
    }
}