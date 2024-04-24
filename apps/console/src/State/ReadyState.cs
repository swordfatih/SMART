namespace Player
{
    public class ReadyState : IState
    {
        public Action Action(Player player)
        {
            return new IdleAction(player);
        }
    }
}