namespace Player
{
    public enum Team
    {
        Solo,
        Prisoner,
        Associate
    }

    public class Player(string name)
    {
        public string Name { get; set; } = name;
        public IPlayerState State { get; set; } = new ReadyState();
        public Team Team { get; set; } = Team.Solo;
        public int Position { get; set; }
        public int Progression { get; set; }

        public override string ToString()
        {
            return Name + " (" + Team.ToString() + ")";
        }
    }
}