namespace Player
{
    public class GuardState : IPlayerState
    {
        public Action.Action Action(Player player)
        {
            Console.WriteLine("1. Ne rien faire");

            if (player.Team == Team.Associate)
            {
                Console.WriteLine("2. Dénoncer un prisonnier");

                var choice = Console.ReadLine();

                if (choice == "2")
                {
                    Console.WriteLine("Le numéro du prisonnier cible: ");

                    var target = Convert.ToInt32(Console.ReadLine());

                    return new Action.RedirectGuardAction(player, target);
                }
            }
            else
            {
                Console.WriteLine("2. Retenir le gardien");

                var choice = Console.ReadLine();

                if (choice == "2")
                {
                    return new Action.DelayGuardAction(player);
                }
            }

            return new Action.IdleAction(player);
        }
    }
}