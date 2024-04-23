namespace Action
{
    public class DelayGuardAction : Action
    {
        public DelayGuardAction(Player.Player player) : base(player)
        {

        }

        public override void Run(Game game)
        {

        }

        public override string ToString()
        {
            return Player.Name + " delays the guard";
        }
    }
}