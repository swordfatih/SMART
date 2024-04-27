namespace Game
{
    public class DelayGuardAction(Player player) : Action(player)
    {
        public override void Run(Board board)
        {

        }

        public override string ToString()
        {
            return $"{Player} delays the guard";
        }
    }
}