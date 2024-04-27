namespace Game
{
    public abstract class Action(Player player)
    {
        public Player Player { get; } = player;

        public abstract void Run(Board board);

        public abstract override string ToString();
    }
}