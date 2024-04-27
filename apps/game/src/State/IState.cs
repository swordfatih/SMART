namespace Game
{
    public interface IState
    {
        public Action Action(Board board, Player player);
    }
}