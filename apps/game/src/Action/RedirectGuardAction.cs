namespace Game
{
    public class RedirectGuardAction : Action
    {
        private readonly Player Target;

        public RedirectGuardAction(Player player, Player target) : base(player)
        {
            Target = target;
        }

        public override void Run(Board board)
        {
            board.NextGuardPosition = Target.Position;
        }

        public override string ToString()
        {
            return $"redirect_action:{Player.Client.Name},{Target.Client.Name}";
        }
    }
}