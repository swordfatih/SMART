namespace Game
{
    public class VoteAction : Action
    {
        private readonly Player Target;

        public VoteAction(Player player, Player target) : base(player)
        {
            Target = target;
        }
        
        public override void Run(Board board)
        {
            board.Votes![Target.Position]++;
        }

        public override string ToString()
        {
            // return $"{Player} redirects the guard to {Target.Position}";
            return $"vote_action:{Player.Client.Name},{Target.Client.Name}";
        }
    }
}