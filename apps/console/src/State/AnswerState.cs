namespace Player
{
    public class AnswerState(Communication communication) : IState
    {
        private Communication Communication { get; } = communication;

        public Action Action(Player player)
        {
            Console.WriteLine(player.Name + " received a message from " + Communication.Origin.Name);
            Console.WriteLine("Accept ? Y/N");

            var accept = Console.ReadLine() ?? "N";

            if (accept == "Y")
            {
                Console.WriteLine(Communication.Request);

                if (Communication is MessageCommunication c1)
                {
                    Console.WriteLine(c1.Message);
                }
                else if (Communication is ItemCommunication c2)
                {
                    return new ReceiveDonationAction(player, c2);
                }
                else if (Communication is ChoiceCommunication c3)
                {
                    Console.WriteLine(c3.Question);
                }

                return new IdleAction(player);
            }

            return new PropagateAction(player, Communication);
        }
    }
}