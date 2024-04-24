namespace Player
{
    public abstract class Communication(Player origin, Direction direction)
    {
        public abstract string Question { get; init; }
        public abstract List<string> Answers { get; init; }
        public Player Origin { get; } = origin;
        public Direction Direction { get; } = direction;
    }
}