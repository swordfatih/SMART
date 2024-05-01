namespace Game
{
    public class AssociateRole : Role
    {
        public AssociateRole() : base(Team.Associate)
        {
        }

        public override string ToString()
        {
            return "Associate";
        }
    }
}