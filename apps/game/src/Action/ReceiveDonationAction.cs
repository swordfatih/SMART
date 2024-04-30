namespace Game
{
    public class ReceiveDonationAction : Action
    {
        private ItemCommunication Communication { get; }

        public ReceiveDonationAction(Player player, ItemCommunication communication) : base(player)
        {
            Communication = communication;
        }

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