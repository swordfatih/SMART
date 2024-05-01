namespace Game
{
    public class PlayerData
    {
        public Player Player { get; set; }
        public bool HasGuard { get; set; }

        public PlayerData(Player player, bool hasGuard)
        {
            Player = player;
            HasGuard = hasGuard;
        }
    }
}