using System.Collections.Generic;

namespace Game
{
    public class PlayerData
    {
        public Player Player { get; set; }
        public bool HasGuard { get; set; }
        public int Day { get; set; }
        public List<PlayerPositionData> Positions { get; set; }

        public PlayerData(Player player, bool hasGuard, int day, List<PlayerPositionData> positions)
        {
            Player = player;
            HasGuard = hasGuard;
            Day = day;
            Positions = positions;
        }
    }
}