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
                var tree = Communication.Tree(player);
                Console.WriteLine(tree);

                while (tree.Answers.Count != 0)
                {
                    var choice = Convert.ToInt32(Console.ReadLine());
                    var instruction = tree.Answers[choice];

                    if (instruction is Question question)
                    {
                        tree = question;
                    }
                    else if (instruction is Answer answer)
                    {
                        return answer.Action;
                    }
                }

                return new IdleAction(player);
            }

            return new PropagateAction(player, Communication);
        }
    }
}