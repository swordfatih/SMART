namespace Action
{
    public class DigAction : Action
    {
        public DigAction(Player.Player player) : base(player)
        {

        }

        public override void Run(Game game)
        {
            Player.Progression++;
        }

        public override string ToString()
        {
            return Player.Name + " digs (" + Player.Progression + ")";
        }
    }
}