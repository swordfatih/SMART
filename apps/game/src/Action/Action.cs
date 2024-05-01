namespace Game
{
    public abstract class Action
    {
        public Player Player { get; }

        public Action(Player player)
        {
            Player = player;
        } 

        public abstract void Run(Board board);

        public abstract override string ToString();
    }
}