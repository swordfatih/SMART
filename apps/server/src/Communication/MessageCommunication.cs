namespace Board
{
    public class MessageCommunication(Player origin, Direction direction, string message) : Communication(origin, direction)
    {
        public readonly string Message = message;
    }
}