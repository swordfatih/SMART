namespace Game
{
    public class UsePoisonAction : Action
    {
        public UsePoisonAction(Player player) : base(player)
        {
        }

        public override void Run(Board board)
        {
            Player.Items.Remove(Player.Items.Find(x => x is PoisonItem));
            Player.Status = Status.Dead;
        }

        public override string ToString()
        {
            return $"use_soap:{Player.Client.Name}";
        }
    }
}