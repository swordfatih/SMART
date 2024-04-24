namespace Player
{
    public class AssociateRole : Role
    {
        public override Team Team { get; set; } = Team.Associate;

        public override string ToString()
        {
            return "Associate";
        }
    }
}