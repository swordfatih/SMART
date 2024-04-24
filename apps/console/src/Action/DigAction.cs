namespace Player
{
    public class DigAction(Player player) : Action(player)
    {
        public override void Run(Game.Game game)
        {
            Player.Progression++;
        }

        public override string ToString()
        {
            return Player.Name + " digs (" + Player.Progression + ")";
        }
    }
}