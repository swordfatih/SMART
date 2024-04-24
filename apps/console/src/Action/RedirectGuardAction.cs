namespace Player
{
    public class RedirectGuardAction(Player player, int next) : Action(player)
    {
        private int NextPosition { get; init; } = next;

        public override void Run(Game.Game game)
        {
            game.NextGuardPosition = NextPosition;
        }

        public override string ToString()
        {
            return Player.Name + " redirects the guard to " + NextPosition;
        }
    }
}