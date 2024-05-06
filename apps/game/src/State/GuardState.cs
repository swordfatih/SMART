using System.Linq;

namespace Game
{
    public class GuardState : State
    {
        public override Action Action(Board board, Player player)
        {
            if (player.Role.Team == Team.Associate)
            {
                var targets = board.Players.Except(Team.Associate).Except(Status.Dead).Except(Status.Escaped).Except(player);
                return new RedirectGuardAction(player, targets.ElementAt(player.Client.SendChoice(new("Vers quel joueur rediriger le gardien ?", new(targets.Select(x => x.Client.Name))))));
            }

            return new IdleAction(player);
        }
    }
}