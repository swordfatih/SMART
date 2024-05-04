namespace Game
{
    public class PropagateAction : Action
    {
        private Communication Communication { get; }

        public PropagateAction(Player player, Communication communication) : base(player)
        {
            Communication = communication;
        }

        public override void Run(Board board)
        {
            var target = board.Players.Only(Status.Alive).AdjacentPlayer(Player, Communication.Direction);

            if (target != Communication.Origin)
            {
                target?.States.Push(new AnswerState(Communication));
            }
        }

        public override string ToString()
        {
            // return $"{Player} propagates communication to the {Communication.Direction}";
            return $"propagate_action:{Player.Client.Name},";
        }
    }
}