namespace Game
{
    public class IdleAction : Action
    {
        public IdleAction(Player player) : base(player)
        {
        }

        public override void Run(Board board)
        {

        }

        public override string ToString()
        {
            // return $"{Player} does nothing";
            return $"idle_action:{Player.Client.Name}";
        }
    }
}