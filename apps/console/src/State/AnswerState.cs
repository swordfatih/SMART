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
                Console.WriteLine(Communication.Question);

                for (var i = 0; i < Communication.Answers.Count; ++i)
                {
                    Console.WriteLine(i + ". " + Communication.Answers[i]);
                }

                var choice = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Answer: " + Communication.Answers[choice]);

                return new IdleAction(player);
            }
            
            return new PropagateAction(player, Communication);
        }
    }
}