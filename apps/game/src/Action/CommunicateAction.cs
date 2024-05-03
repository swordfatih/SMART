namespace Game
{
    public class CommunicateAction : Action
    {
        private Communication Communication { get; }

        public CommunicateAction(Player player, Communication communication) : base(player)
        {
            Communication = communication;
        }   

        public override void Run(Board board)
        {
            var target = board.Players.AdjacentPlayer(Player, Communication.Direction);
            target.States.Push(new AnswerState(Communication));
        }

        public override string ToString()
        {
            return $"{Player} communicates to the {Communication.Direction}";
        }
    }
}