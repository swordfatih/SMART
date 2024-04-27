namespace Game
{
    public class IdleAction(Player player) : Action(player)
    {
        public override void Run(Board board)
        {

        }

        public override string ToString()
        {
            return $"{Player} does nothing";
        }
    }
}