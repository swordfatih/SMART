namespace Player
{
    public abstract class Action(Player player)
    {
        protected Player Player { get; } = player;

        public abstract void Run(Game.Game game);

        public abstract override string ToString();
    }
}