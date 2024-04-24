namespace Player
{
    public class MessageCommunication(Player origin, Direction direction, Request request, string message) : Communication(origin, direction, request)
    {
        public readonly string Message = message;
    }
}