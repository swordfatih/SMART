namespace Game
{
    public class DieAction(Player player) : Action(player)
    {
        public override void Run(Board board)
        {
            Player.Status = Status.Dead;
        }

        public override string ToString()
        {
            return $"{Player} dies";
        }
    }
}