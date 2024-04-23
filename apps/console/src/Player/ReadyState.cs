using Action;

namespace Player
{
    public class ReadyState : IPlayerState
    {
        public Action.Action Action(Player player)
        {
            return new IdleAction(player);
        }
    }
}