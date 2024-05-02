using System.Linq;

namespace Game
{
    public class SafeState : State
    {
        public override Action Action(Board board, Player player)
        {
            var choice = player.Client.AskChoice(new("Choisissez l'action du jour", new() { "Creuser", "Communiquer avec un voisin" }));

            if (choice == 0)
            {
                return board.GuardPosition == player.Position ? new DieAction(player) : new DigAction(player);
            }
            else if (choice == 1)
            {
                var direction = player.Client.AskChoice(new("Communiquer avec le voisin de", new() { "Gauche", "Droite" })) == 0 ? Direction.Left : Direction.Right;

                var question = new Question("Que voulez-vous communiquer ?", new() { "Gardien", "Opinion", "Progression", "Message" });

                if (player.Items.Count > 0)
                {
                    question.Answers.Add("Donation");
                }

                choice = player.Client.AskChoice(question);

                Communication? communication = null;
                switch (question.Answers[choice])
                {
                    case "Donation":
                        var item = player.Items[player.Client.AskChoice(new("Choisissez votre objet", new(player.Items.Select(x => x.Name))))];
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