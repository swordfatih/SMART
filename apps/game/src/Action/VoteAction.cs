namespace Game
{
    public class VoteAction : Action
    {
        private readonly int Choice;
        private string? Name;

        public VoteAction(Player player, int choice) : base(player)
        {
            Choice = choice;
        }

        public override void Run(Board board)
        {
           // board.votes[Choice]++;
            Name = board.Players[Choice].Client.Name;
        }

        public override string ToString()
        {
            // return $"{Player} redirects the guard to {Target.Position}";
            return $"vote_action:{Player.Client.Name},{Name}";
        }
    }
}