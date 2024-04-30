namespace Game
{
    public class DelayGuardAction : Action
    {
        public DelayGuardAction(Player player) : base(player)
        {
        }

        public override void Run(Board board)
        {

        }

        public override string ToString()
        {
            // return $"{Player} delays the guard";
            return $"delay_action:{Player.Client.Name},";
        }
    }
}