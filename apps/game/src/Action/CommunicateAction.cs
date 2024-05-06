namespace Game
{
    public class CommunicateAction : Action
    {
        private Communication Communication { get; }
        private Player? Target { get; set; }

        public CommunicateAction(Player player, Communication communication) : base(player)
        {
            Communication = communication;
        }   

        public override void Run(Board board)
        {
            Target = board.Players.Only(Status.Alive).AdjacentPlayer(Player, Communication.Direction);
            Target?.States.Push(new AnswerState(Communication));
        }

        public override string ToString()
        {
            if(Target is not null)
            {
                return $"communicate_action:{Player.Client.Name},{Communication.Direction},{Target.Client.Name}";
            }

            return $"communicate_action:{Player.Client.Name},{Communication.Direction}";
        }
    }
}