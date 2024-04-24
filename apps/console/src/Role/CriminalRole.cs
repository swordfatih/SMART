namespace Player
{
    public class CriminalRole : Role
    {
        public override Team Team { get; set; } = Team.Criminal;

        public override string ToString()
        {
            return "Criminal";
        }
    }
}