namespace Game
{
    public abstract class Communication
    {
        public readonly Player Origin;
        public readonly Direction Direction;

        public Communication(Player origin, Direction direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}