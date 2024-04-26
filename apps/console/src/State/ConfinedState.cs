namespace Board
{
    public class ConfinedState : IState
    {
        public Action Action(Game game, Player player)
        {
            return new IdleAction(player);
        }
    }
}