namespace Board
{
    public abstract class Communication(Player origin, Direction direction)
    {
        public readonly Player Origin = origin;
        public readonly Direction Direction = direction;
    }
}