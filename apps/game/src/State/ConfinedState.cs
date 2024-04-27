namespace Game
{
    public class ConfinedState : IState
    {
        public Action Action(Board board, Player player)
        {
            return new IdleAction(player);
        }
    }
}