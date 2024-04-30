namespace Game
{
    public class MessageCommunication : Communication
    {
        public readonly string Message;

        public MessageCommunication(Player origin, Direction direction, string message) : base(origin, direction)
        {
            Message = message;
        }
    }
}