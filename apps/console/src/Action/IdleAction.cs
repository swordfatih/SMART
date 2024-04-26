namespace Board
{
    public class IdleAction(Player player) : Action(player)
    {
        public override void Run(Game game)
        {

        }

        public override string ToString()
        {
            return $"{Player} does nothing";
        }
    }
}