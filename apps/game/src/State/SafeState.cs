using System.Linq;

namespace Game
{
    public class SafeState : State
    {
        public override Action Action(Board board, Player player)
        {
            var action = player.Client.SendChoice(new("Choisissez l'action du jour", new() { "Creuser", "Communiquer avec un voisin" }));

            if (action == 0)
            {
                return board.GuardPosition == player.Position ? new DieAction(player) : new DigAction(player);
            }
            else if (action == 1)
            {
                var direction = player.Client.SendChoice(new("Communiquer avec le voisin de", new() { "Gauche", "Droite" })) == 0 ? Direction.Left : Direction.Right;

                var choice = new Choice("Que voulez-vous communiquer ?", new() { "Gardien", "Opinion", "Progression", "Message" });

                if (player.Items.Count > 0)
                {
                    choice.Answers.Add("Donation");
                }

                var answer = player.Client.SendChoice(choice);

                Communication? communication = null;
                switch (choice.Answers[answer])
                {
                    case "Donation":
                        var items = player.Items.Select(x => x.Name).ToList();

                        if(player.Role.Team == Team.Inmate)
                        {
                            items.Remove("Poison");
                        }

                        var item = player.Items[player.Client.SendChoice(new("Choisissez votre objet", items))];
                        communication = new ItemCommunication(player, direction, item);
                        break;
                    case "Message":
                        communication = new MessageCommunication(player, direction, player.Client.AskInput("Entrez votre message"));
                        break;
                    case "Gardien":
                        communication = new ChoiceCommunication(player, direction, new("Le gardien est-il devant toi ?", new() { "Oui", "Non" }));
                        break;
                    case "Progression":
                        communication = new ChoiceCommunication(player, direction, new("Veux-tu partager ta progression ?", new() { "Oui", "Non" }));
                        break;
                    case "Opinion":
                        communication = new ChoiceCommunication(player, direction, new("Ton avis sur ton autre voisin ?", new() { "Malveillance max", "Un bon", "Je ne sais pas" }));
                        break;
                    default:
                        break;
                }

                if (communication != null)
                {
                    return new CommunicateAction(player, communication);
                }
            }

            return new IdleAction(player);
        }
    }
}