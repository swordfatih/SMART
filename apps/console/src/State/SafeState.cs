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

                Console.WriteLine("1. Est-ce que le gardien est devant ta cellule ? (oui / non)");
                Console.WriteLine("2. Ton avis sur la personne dans l\'autre cellule ?  (taupe, prisonnier, solo, avec possibilité de choisir un rôle en particulier si on sait, je ne sais pas)");
                Console.WriteLine("3. Donner/racketter un objet");
                Console.WriteLine("4. Fais passer un \"message\" (oui / non)");
                Console.WriteLine("5. Demander à partager son avancement (les 2 se le disent ou personne ne dit rien)");

                var question = Console.ReadLine();
                Communication communication;

                switch (question)
                {
                    case "1":
                        communication = new GuardCommunication(player, direction);
                        break;
                    case "5":
                        communication = new ProgressionCommunication(player, direction);
                        break;
                    default:
                        communication = new GuardCommunication(player, direction);
                        break;
                }

                return new CommunicateAction(player, communication);
            }

            return new IdleAction(player);
        }
    }
}