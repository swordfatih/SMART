namespace Board
{
    public abstract class Action(Player player)
    {
        public Player Player { get; } = player;

        public abstract void Run(Game game);

        public abstract override string ToString();
    }
}