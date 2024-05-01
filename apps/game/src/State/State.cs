namespace Game
{
    public class State
    {
        public virtual Action Action(Board board, Player player)
        {
            return new IdleAction(player);
        }
    }
}