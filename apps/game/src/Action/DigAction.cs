namespace Game
{
    public class DigAction(Player player) : Action(player)
    {
        public override void Run(Board board)
        {
            Player.Progression++;
        }

        public override string ToString()
        {
            return $"{Player} digs ({Player.Progression})";
        }
    }
}