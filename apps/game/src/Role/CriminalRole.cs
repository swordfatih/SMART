namespace Game
{
    public class CriminalRole : Role
    {
        public CriminalRole() : base(Team.Criminal)
        {
        }

        public override string ToString()
        {
            return "Criminal";
        }
    }
}