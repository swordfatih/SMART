namespace Game
{
    public class ConfinedState : State
    {
        public override Action Action(Board board, Player player)
        {
            return new IdleAction(player);
        }
    }
}