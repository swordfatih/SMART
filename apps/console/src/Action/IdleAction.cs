namespace Player
{
    public class IdleAction(Player player) : Action(player)
    {
        public override void Run(Game.Game game)
        {

        }

        public override string ToString()
        {
            return Player.Name + " does nothing";
        }
    }
}