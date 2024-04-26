namespace Board
{
    public class RedirectGuardAction(Player player, Player target) : Action(player)
    {
        public override void Run(Game game)
        {
            game.NextGuardPosition = target.Position;
        }

        public override string ToString()
        {
            return $"{Player} redirects the guard to {target.Position}";
        }
    }
}