namespace Player
{
    public abstract class Communication(Player origin, Direction direction, Request request)
    {
        public readonly Player Origin = origin;
        public readonly Direction Direction = direction;
        public readonly Request Request = request;
    }
}