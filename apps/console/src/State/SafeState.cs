namespace Board
{
    public class SafeState : IState
    {
        public Action Action(Game game, Player player)
        {
            var choice = player.Client.AskChoice(new("Choisissez l'action du jour", ["Creuser", "Communiquer avec un voisin"]));

            if (choice == 0)
            {
                return new DigAction(player);
            }
            else if (choice == 1)
            {
                var direction = player.Client.AskChoice(new("Communiquer avec le voisin de", ["Gauche", "Droite"])) == 0 ? Direction.Left : Direction.Right;

                var question = new Question("Que voulez-vous communiquer ?", [
                    "Gardien",
                    "Opinion",
                    "Progression",
                    "Message"
                ]);

                if(player.Items.Count > 0)
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
                    case "Guard":
                        communication = new ChoiceCommunication(player, direction, new("Le gardien est-il devant toi ?", ["Oui", "Non"]));
                        break;
                    case "Progression":
                        communication = new ChoiceCommunication(player, direction, new("Veux-tu partager ta progression ?", ["Oui", "Non"]));
                        break;
                    case "Opinion":
                        communication = new ChoiceCommunication(player, direction, new("Ton avis sur ton autre voisin ?", ["Malveillance max", "Un bon", "Je ne sais pas"]));
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