namespace Player
{
    public class DelayGuardAction(Player player) : Action(player)
    {
        public override void Run(Game.Game game)
        {

        }

        public override string ToString()
        {
            return Player.Name + " delays the guard";
        }
    }
}