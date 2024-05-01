namespace Game
{
    public enum Team
    {
        Solo,
        Criminal,
        Associate
    }

    public class Role
    {
        public Team Team { get; set; }

        public Role(Team team)
        {
            Team = team;
        }
    }
}