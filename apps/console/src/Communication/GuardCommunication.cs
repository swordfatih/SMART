namespace Player
{
    public class GuardCommunication(Player origin, Direction direction) : Communication(origin, direction)
    {
        public override Question Tree(Player player)
        {
            return new("Avez-vous le gardien devant vous ?", [
                new Answer("Oui", new IdleAction(player)),
                new Answer("Non", new IdleAction(player)),
            ]);
        }
    }
}