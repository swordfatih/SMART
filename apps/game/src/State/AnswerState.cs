namespace Game
{
    public class AnswerState : IState
    {
        private Communication Communication { get; }

        public AnswerState(Communication communication) 
        {
            Communication = communication;
        }

        public Action Action(Board board, Player player)
        {
            if (player.Client.AskChoice(new("You received a message from " + Communication.Origin, new(){"Accept", "Propagate"})) == 1)
            {
                return new PropagateAction(player, Communication);
            }

            if (Communication is MessageCommunication c1)
            {
                player.Client.SendMessage(c1.Message);
            }
            else if (Communication is ItemCommunication c2)
            {
                return new ReceiveDonationAction(player, c2);
            }
            else if (Communication is ChoiceCommunication c3)
            {
                var choice = player.Client.AskChoice(c3.Question);
                c3.Origin.Client.SendMessage($"{player.Client.Name} answers {c3.Question.Answers[choice]}");
            }

            return new IdleAction(player);
        }
    }
}