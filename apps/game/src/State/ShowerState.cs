using System.Linq;

namespace Game
{
    public class ShowerState : IState
    {
        public Action Action(Board board, Player player)
        {
            var targets = board.GetAlivePlayers(player);
            return new VoteAction(player, targets[player.Client.AskChoice(new("Contre qui vous voulez voter ?", new(targets.Select(x => x.Client.Name))))]);
        }
    }
}