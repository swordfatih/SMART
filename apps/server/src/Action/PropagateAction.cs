namespace Board
{
    public class PropagateAction(Player player, Communication communication) : Action(player)
    {
        private Communication Communication { get; } = communication;

        public override void Run(Game game)
        {
            var target = game.AdjacentPlayer(Player, Communication.Direction);

            if (target != Communication.Origin)
            {
                target.State.Push(new AnswerState(Communication));
            }
        }

        public override string ToString()
        {
            return $"{Player} propagates communication to the {Communication.Direction}";
        }
    }
}