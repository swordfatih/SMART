namespace Board
{
    public class DelayGuardAction(Player player) : Action(player)
    {
        public override void Run(Game game)
        {

        }

        public override string ToString()
        {
            return $"{Player} delays the guard";
        }
    }
}