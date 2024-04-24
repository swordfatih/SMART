namespace Player
{
    public class ProgressionCommunication(Player origin, Direction direction) : Communication(origin, direction)
    {
        public override string Question { get; init; } = "Voulez-vous partager votre avancement ?";
        public override List<string> Answers { get; init; } = ["Oui", "Non"];
    }
}