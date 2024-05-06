namespace Game
{
    public class ConfinedState : State
    {
        public override Action Action(Board board, Player player)
        {
            player.Status = Status.Alive;
            return new IdleAction(player);
        }
    }
}