namespace Board
{
    public class RedirectGuardAction(Player player, Player target) : Action(player)
    {
        public override void Run(Game game)
        {
            game.NextGuardPosition = target.Position;
            target.Status = Status.Confined;
        }

        public override string ToString()
        {
            return $"{Player} redirects the guard to {target.Position}";
        }
    }
}