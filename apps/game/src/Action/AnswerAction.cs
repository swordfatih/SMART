namespace Game
{
    public class AnswerAction : Action
    {
        public Communication Communication { get; }

        public AnswerAction(Player player, Communication communication) : base(player)
        {
            Communication = communication;
        } 

        public override void Run(Board board)
        {
            
        }

        public override string ToString()
        {
            // return $"{Player} answers a communication";
            return $"answer_Action:{Player.Client.Name},";
        }
    }
}