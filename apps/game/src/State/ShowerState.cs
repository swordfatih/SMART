using System.Linq;

namespace Game
{
    public class ShowerState : State
    {
        public override Action Action(Board board, Player player)
        {
            if (player.Items.Any(x => x is SoapItem))
            {
                return new UseSoapAction(player);
            }

            var targets = board.Players.Except(Status.Dead).Except(Status.Escaped).Except(player);
            return new VoteAction(player, targets.ElementAt(player.Client.SendChoice(new("Contre qui vous voulez voter ?", new(targets.Select(x => x.Client.Name))))));
        }
    }
}