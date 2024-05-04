namespace Game
{
    public class AnswerState : State
    {
        private Communication Communication { get; }

        public AnswerState(Communication communication) 
        {
            Communication = communication;
        }

        public override Action Action(Board board, Player player)
        {
            if (player.Client.AskChoice(new("You received a message from " + Communication.Origin.Client.Name, new(){"Accept", "Propagate"})) == 1)
            {
                return new PropagateAction(player, Communication);
            }

            if (Communication is MessageCommunication c1)
            {
                player.Client.SendPlayerMessage(c1.Origin.Client.Name, c1.Message);
            }
            else if (Communication is ItemCommunication c2)
            {
                return new ReceiveDonationAction(player, c2);
            }
            else if (Communication is ChoiceCommunication c3)
            {
                var choice = player.Client.AskChoice(c3.Question);
                c3.Origin.Client.SendChoiceAnswer(player.Client.Name, c3.Question, choice);
            }

            return new IdleAction(player);
        }
    }
}