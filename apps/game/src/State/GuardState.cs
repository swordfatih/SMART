namespace Game
{
    public class GuardState : IState
    {
        public Action Action(Board board, Player player)
        {
            if (player.Role.Team == Team.Associate)
            {
                var targets = board.GetAlivePlayers(player);
                return new RedirectGuardAction(player, targets[player.Client.AskChoice(new("Vers quel joueur rediriger le gardien ?", new(targets.Select(x => x.Client.Name))))]);
            }

            return new IdleAction(player);
        }
    }
}