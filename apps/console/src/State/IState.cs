namespace Player
{
    public interface IState
    {
        public Action Action(Player player);
    }
}