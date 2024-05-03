namespace Game
{
    public class InmateRole : Role
    {
        public InmateRole() : base(Team.Inmate)
        {
        }

        public override string ToString()
        {
            return "Inmate";
        }
    }
}