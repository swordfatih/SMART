namespace Player
{
    public interface IPlayerState
    {
        public Action.Action Action(Player player);
    }
}