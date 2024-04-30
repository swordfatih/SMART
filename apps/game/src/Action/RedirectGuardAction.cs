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
            Target.Status = Status.Confined;
        }

        public override string ToString()
        {
            return $"{Player} redirects the guard to {Target.Position}";
        }
    }
}