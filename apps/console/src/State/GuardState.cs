namespace Player
{
    public class GuardState : IState
    {
        public Action Action(Player player)
        {
            Console.WriteLine("1. Ne rien faire");

            if (player.Role.Team == Team.Associate)
            {
                Console.WriteLine("2. Dénoncer un prisonnier");

                var choice = Console.ReadLine();

                if (choice == "2")
                {
                    Console.WriteLine("Le numéro du prisonnier cible: ");

                    var target = Convert.ToInt32(Console.ReadLine());

                    return new RedirectGuardAction(player, target);
                }
            }

            return new IdleAction(player);
        }
    }
}