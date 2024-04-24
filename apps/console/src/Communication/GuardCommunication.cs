namespace Player
{
    public class GuardCommunication(Player origin, Direction direction) : Communication(origin, direction)
    {
        public override string Question { get; init; } = "Avez-vous le gardien devant vous ?";
        public override List<string> Answers { get; init; } = ["Oui", "Non"];
    }
}