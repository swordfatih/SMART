namespace Board
{
    public interface IState
    {
        public Action Action(Game game, Player player);
    }
}