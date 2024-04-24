namespace Player
{
    public class PropagateAction(Player player, Communication communication) : Action(player)
    {
        private Communication Communication { get; } = communication;

        public override void Run(Game.Game game)
        {
            game.AdjacentPlayer(Player, Communication.Direction).State.Push(new AnswerState(Communication));
        }

        public override string ToString()
        {
            return Player.Name + " propagates communication (" + Communication + ")";
        }
    }
}