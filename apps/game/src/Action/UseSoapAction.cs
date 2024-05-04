namespace Game
{
    public class UseSoapAction : Action
    {
        public UseSoapAction(Player player) : base(player)
        {
        }

        public override void Run(Board board)
        {
        }

        public override string ToString()
        {
            return $"use_soap:{Player.Client.Name}";
        }
    }
}