namespace Game
{
    public class PlayerData
    {
        public readonly Player Player;
        public readonly bool HasGuard;

        public PlayerData(Player player, bool hasGuard)
        {
            Player = player;
            HasGuard = hasGuard;
        }
    }
}