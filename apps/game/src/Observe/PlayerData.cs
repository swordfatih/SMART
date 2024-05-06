namespace Game
{
    public class PlayerData
    {
        public Player Player { get; set; }
        public bool HasGuard { get; set; }
        public int Day { get; set; }

        public PlayerData(Player player, bool hasGuard, int day)
        {
            Player = player;
            HasGuard = hasGuard;
            Day = day;
        }
    }
}