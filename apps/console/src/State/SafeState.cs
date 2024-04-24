namespace Player
{
    public class SafeState : IState
    {
        public Action Action(Player player)
        {
            Console.WriteLine("1. Creuser");
            Console.WriteLine("2. Communiquer avec un voisin");

            var choice = Console.ReadLine();

            if (choice == "1")
            {
                return new DigAction(player);
            }
            else if (choice == "2")
            {
                Console.WriteLine("Communiquer avec le prisonnier de gauche ou de droite ? (G/D)");

                var direction = Console.ReadLine() == "G" ? Direction.Left : Direction.Right;

                List<Communication> communications = [
                    new ProgressionCommunication(player, direction),
                    new GuardCommunication(player, direction)
                ];

                for (var i = 0; i < communications.Count; ++i)
                {
                    Console.WriteLine(i + ". " + communications[i].Tree(player).Value);
                }

                var communication = communications[Convert.ToInt32(Console.ReadLine())];
                return new CommunicateAction(player, communication);
            }

            return new IdleAction(player);
        }
    }
}