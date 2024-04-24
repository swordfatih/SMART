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

                var requests = (Request[])Enum.GetValues(typeof(Request));
                for (var i = 0; i < requests.Length; ++i)
                {
                    Console.WriteLine(i + ". " + requests[i].ToString());
                }

                var request = requests[Convert.ToInt32(Console.ReadLine())];

                Communication? communication = null;

                switch (request)
                {
                    case Request.Donate:
                        for (var i = 0; i < player.Items.Count; ++i)
                        {
                            Console.WriteLine(i + ". " + player.Items[i]);
                        }

                        var item = player.Items[Convert.ToInt32(Console.ReadLine())];
                        communication = new ItemCommunication(player, direction, request, item);

                        break;
                    case Request.Message:
                        communication = new MessageCommunication(player, direction, request, Console.ReadLine() ?? "");
                        break;
                    case Request.Guard:
                        communication = new ChoiceCommunication(player, direction, request, new Question("Le gardien est-il devant toi ?", ["Oui", "Non"]));
                        break;
                    case Request.Progression:
                        communication = new ChoiceCommunication(player, direction, request, new Question("Veux-tu partager ta progression ?", ["Oui", "Non"]));
                        break;
                    case Request.Opinion:
                        communication = new ChoiceCommunication(player, direction, request, new Question("Ton avis sur ton autre voisin ?", ["Malveillance max", "Un bon", "Je ne sais pas"]));
                        break;
                    default:
                        break;
                }

                if (communication != null)
                {
                    return new CommunicateAction(player, communication);
                }
            }

            return new IdleAction(player);
        }
    }
}