namespace Game
{
    public class DigAction : Action
    {
        public DigAction(Player player) : base(player)
        {
        }

        public override void Run(Board board)
        {
            Player.Progression++;
        }

        public override string ToString()
        {
            //return $"{Player} digs ({Player.Progression})";
            return $"dig_action:,{Player.Client.Name}";
        }
    }
}