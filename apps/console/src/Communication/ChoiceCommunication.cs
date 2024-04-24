namespace Player
{
    public class ChoiceCommunication(Player origin, Direction direction, Request request, Question question) : Communication(origin, direction, request)
    {
        public readonly Question Question = question;
    }
}