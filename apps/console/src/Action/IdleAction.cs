namespace Action
{
    public class IdleAction : Action
    {
        public IdleAction(Player.Player player) : base(player)
        {

        }

        public override void Run(Game game)
        {

        }

        public override string ToString()
        {
            return Player.Name + " does nothing";
        }
    }
}