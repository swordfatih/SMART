namespace Player
{
    public class CommunicateAction(Player player, Communication communication) : Action(player)
    {
        private Communication Communication { get; } = communication;

        public override void Run(Game.Game game)
        {
            var target = game.AdjacentPlayer(Player, Communication.Direction);
            target.State.Push(new AnswerState(Communication));
        }

        public override string ToString()
        {
            return Player.Name + " communicates to the " + Communication.Direction.ToString() + " (" + Communication.Request + ")";
        }
    }
}