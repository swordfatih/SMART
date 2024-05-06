namespace Game
{
    public class DieAction : Action
    {
        public DieAction(Player player) : base(player)
        {
        }

        public override void Run(Board board)
        {
            Player.Status = Status.Dead;
        }

        public override string ToString()
        {
            return $"die_action:{Player.Client.Name}";
        }
    }
}