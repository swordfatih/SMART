namespace Player
{
    public class ReadyState : IPlayerState
    {
        public Action.Action Action(Player player)
        {
            return new Action.IdleAction(player);
        }
    }
}