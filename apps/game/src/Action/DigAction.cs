namespace Game
{
    public class DigAction(Player player) : Action(player)
    {
        public override void Run(Board board)
        {
            if (board.GuardPosition == Player.Position)
            {
                Player.Status = Status.Dead;
            }
            else
            {
                Player.Progression++;
            }
        }

        public override string ToString()
        {
            return $"{Player} digs ({Player.Progression})";
        }
    }
}