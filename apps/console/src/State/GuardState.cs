namespace Board
{
    public class GuardState : IState
    {
        public Action Action(Game game, Player player)
        {
            if (player.Role.Team == Team.Associate)
            {
                var targets = game.GetAlivePlayers(player);
                return new RedirectGuardAction(player, targets[player.Client.AskChoice(new("Vers quel joueur rediriger le gardien ?", new(targets.Select(x => x.Client.Name))))]);
            }

            return new IdleAction(player);
        }
    }
}