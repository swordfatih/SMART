namespace Game
{
    public class PlayerPositionData
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public Status Status { get; set; }
        public Team? Team { get; set; }

        public PlayerPositionData(string name, int position, Status status, Team? team = null)
        {
            Name = name;
            Position = position;
            Status = status;
            Team = team;
        }
    }
}