namespace Game
{
    public enum Team
    {
        Solo,
        Inmate,
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