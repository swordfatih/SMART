namespace Player
{
    public class ProgressionCommunication(Player origin, Direction direction) : Communication(origin, direction)
    {
        public override Question Tree(Player player)
        {
            return new("Voulez-vous partager votre progression ?", [
                new Answer("Oui", new IdleAction(player)),
                new Answer("Non", new IdleAction(player)),
            ]);
        }
    }
}