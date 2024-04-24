namespace Player
{
    public abstract class Communication(Player origin, Direction direction)
    {
        public Player Origin { get; } = origin;
        public Direction Direction { get; } = direction;

        public abstract Question Tree(Player player);
    }
}