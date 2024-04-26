namespace Board
{
    public class ReceiveDonationAction(Player player, ItemCommunication communication) : Action(player)
    {
        private ItemCommunication Communication { get; } = communication;

        public override void Run(Game game)
        {
            Communication.Origin.Items.Remove(Communication.Item);
            Player.Items.Add(Communication.Item);
        }

        public override string ToString()
        {
            return $"{Player} receives a donation";
        }
    }
}