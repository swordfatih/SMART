namespace Player
{
    public enum Team
    {
        Solo,
        Criminal,
        Associate
    }

    public abstract class Role
    {
        public abstract Team Team { get; set; }
    }
}