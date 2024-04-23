namespace Action
{
    public class RedirectGuardAction : Action
    {
        private int NextPosition { get; init; }

        public RedirectGuardAction(Player.Player player, int next) : base(player)
        {
            NextPosition = next;
        }

        public override void Run(Game game)
        {
            game.Guard = NextPosition;
        }

        public override string ToString()
        {
            return Player.Name + " redirects the guard";
        }
    }
}