namespace Board
{
    public class DigAction(Player player) : Action(player)
    {
        public override void Run(Game game)
        {
            if (game.GuardPosition == Player.Position)
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