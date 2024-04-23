namespace Action
{
    public abstract class Action(Player.Player player)
    {
        protected Player.Player Player { get; init; } = player;

        public abstract void Run(Game game);

        public abstract override string ToString();
    }
}