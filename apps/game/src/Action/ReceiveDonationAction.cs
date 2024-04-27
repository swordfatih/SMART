namespace Game
{
    public class ReceiveDonationAction(Player player, ItemCommunication communication) : Action(player)
    {
        private ItemCommunication Communication { get; } = communication;

        public override void Run(Board board)
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