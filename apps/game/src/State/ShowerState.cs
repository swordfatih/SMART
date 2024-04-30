using System.Linq;

namespace Game
{
    public class ShowerState : IState
    {
        public Action Action(Board board, Player player)
        {
            var playerNames = board.Players.Select(p => p.Client.Name).ToList();
            var choice = player.Client.AskChoice(new("Choisissez une personne pour qui voter", playerNames));
            return new VoteAction(player, choice);
        }
    }
}