namespace Game
{
    public class PlayerData(Player player, bool hasGuard)
    {
        public readonly Player Player = player;
        public readonly bool HasGuard = hasGuard;
    }
}