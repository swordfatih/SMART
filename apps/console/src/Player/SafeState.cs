namespace Player
{
    public class SafeState : IPlayerState
    {
        public Action.Action Action(Player player)
        {
            Console.WriteLine("1. Creuser");
            Console.WriteLine("2. Communiquer avec un voisin");

            var choice = Console.ReadLine();

            if(choice == "1")
            {
                return new Action.DigAction(player);
            }
            else if(choice == "2")
            {
                Console.WriteLine("Communiquer avec le prisonnier de gauche ou de droite ? (G/D)");

                var target = Console.ReadLine();

                Console.WriteLine("Est-ce que le gardien est devant ta cellule ? (oui / non)");
                Console.WriteLine("Ton avis sur la personne dans l\'autre cellule ?  (taupe, prisonnier, solo, avec possibilité de choisir un rôle en particulier si on sait, je ne sais pas)");
                Console.WriteLine("Donner/racketter un objet");
                Console.WriteLine("Fais passer un \"message\" (oui / non)");
                Console.WriteLine("Demander à partager son avancement (les 2 se le disent ou personne ne dit rien)");

                // TO-DO : gérer communications
                return new Action.IdleAction(player);
            }

            return new Action.IdleAction(player);
        }
    }
}