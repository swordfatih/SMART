using System;
using System.Collections.Generic;
using System.Linq;
using Game;

namespace Interface
{
    public class PlayerReview
    {
        public int Trust { get; set; }
    }

    public class DecisionClient : Client
    {
        private Dictionary<int, bool> PlayerAnswerGuardien;
        private int PositionRepondeur;
        private Dictionary<int, PlayerReview> PlayerReviews;

        public int LastReceivedPosition { get; set; }
        public BoardData? BoardData { get; set; }
        public PlayerData? PlayerData { get; set; }
        public Direction Direction { get; set; }

        public DecisionClient(string name) : base(name)
        {
            PlayerReviews = new Dictionary<int, PlayerReview>();
            PlayerAnswerGuardien = new Dictionary<int, bool>();
        }

        public DecisionClient() : this(Guid.NewGuid().ToString())
        {
        }

        public override int SendChoice(Choice question)
        {
            var instruction = question.Value.ToString();

            if (instruction.Contains("You received a message from"))
                return AccepterPropager(question);
            else if (instruction.Contains("Vers quel joueur rediriger le gardien"))
                return RedirigerGuardien();
            else if (instruction.Contains("Choisissez l'action du jour"))
                return ChoixAction();
            else if (instruction.Contains("Que voulez-vous communiquer ?"))
                return OptionDemander(question);
            else if (instruction.Contains("Communiquer avec le voisin de"))
                return CommuniquerVoisin();
            else if (instruction.Contains("Le gardien est-il devant toi ?"))
                return GuardienDevant();
            else if (instruction.Contains("Veux-tu partager ta progression ?"))
                return PartagerProgression();
            else if (instruction.Contains("Ton avis sur ton autre voisin ?"))
                return AvisVoisin();
            else if (instruction.Contains("Contre qui vous voulez voter ?"))
                return Voter();

            return 0;
        }

        private int AccepterPropager(Choice question)
        {
            LastReceivedPosition = question.Origin;

            if (new Random().Next(100) < 50)
                return 0;
            else
                return 1;
        }

        private int RedirigerGuardien()
        {
            int trust = 60;
            for (int i = 0; i < PlayerReviews.Count; i++)
            {
                if (PlayerReviews[i].Trust > trust)
                {
                    trust = PlayerReviews[i].Trust;
                    return i;
                }
            }
            return 0;
        }

        public int AttributionTrust()
        {
            for (int i = 0; i < BoardData?.Names.Count; i++)
                PlayerReviews[i].Trust = 60;

            if (PlayerAnswerGuardien[PositionRepondeur])
            {
                if (PlayerData?.HasGuard == true)
                    PlayerReviews[PositionRepondeur].Trust += 10;
                else
                    PlayerReviews[PositionRepondeur].Trust -= 20;
            }
            return 0;
        }

        private int CommuniquerVoisin()
        {
            return new Random().Next(100) < 50 ? 0 : 1;
        }

        private int Voter()
        {
            int Trust = 60;

            if (PlayerData?.Player.Role.Team == Team.Inmate)
            {
                for (int i = 0; i < BoardData?.Names.Count; i++)
                {
                    if (PlayerReviews[i].Trust < Trust)
                        Trust = PlayerReviews[i].Trust;
                }

                for (int j = 0; j < BoardData?.Names.Count; j++)
                {
                    if (Trust == PlayerReviews[j].Trust)
                        return j;
                }
                return 0;
            }
            else
            {
                for (int i = 0; i < BoardData?.Names.Count; i++)
                {
                    if (PlayerReviews[i].Trust > Trust)
                        Trust = PlayerReviews[i].Trust;
                }

                for (int j = 0; j < BoardData?.Names.Count; j++)
                {
                    if (Trust == PlayerReviews[j].Trust)
                        return j;
                }
                return 0;
            }
        }

        // Other methods...

        private int GuardienDevant()
        {
            if (PlayerData?.Player.Role.Team == Team.Inmate)
            {
                if (new Random().Next(100) < 90)
                {
                    if (PlayerData?.HasGuard == true)
                        return 0;
                    else
                        return 1;
                }
                else
                {
                    if (PlayerData?.HasGuard == true)
                        return 1;
                    else
                        return 0;
                }
            }
            else
            {
                if (PlayerData?.HasGuard == true)
                    return 1;
                else
                {
                    if (new Random().Next(100) < 45)
                        return 1;
                    else
                        return 0;
                }
            }
        }

        private int ChoixAction()
        {
            if (PlayerData?.Player.Role.Team == Team.Inmate)
            {
                if (PlayerData?.HasGuard == true)
                    return 0;
                else
                {
                    if (PlayerData?.Player.Progression >= 3)
                        return 1;
                    else
                        return ChoixAction2();
                }
            }
            else
            {
                if (PlayerData?.HasGuard == true)
                    return RedirigerGuardien();
                else
                {
                    if (PlayerData?.Player.Progression < 2)
                    {
                        if (new Random().Next(100) < 50)
                            return 0;
                        else
                            return 1;
                    }
                    else
                    {
                        if (new Random().Next(100) < 90)
                            return 1;
                        else
                            return 0;
                    }
                }
            }
        }

        private int RecevoirObjet()
        {
            if (PlayerReviews[LastReceivedPosition].Trust < 60)
                return RecevoirObjet1();
            else
                return RecevoirObjet2();
        }

        private int RecevoirObjet1()
        {
            if (new Random().Next(100) < 30)
                return 0;
            else
            {
                if (new Random().Next(100) < 50)
                    return 1;
                else
                    return 2;
            }
        }

        private int RecevoirObjet2()
        {
            if (new Random().Next(100) < 50)
                return 0;
            else
            {
                if (new Random().Next(100) < 50)
                    return 1;
                else
                    return 2;
            }
        }

        private int PartagerProgression()
        {
            if (PlayerData?.Player.Progression <= 1)
                return PartagerProgression1();
            else if (PlayerData?.Player.Progression == 2)
                return PartagerProgression2();
            else if (PlayerData?.Player.Progression == 3)
                return PartagerProgression3();
            else
                return 1;
        }

        private int PartagerProgression1()
        {
            if (new Random().Next(100) < 80)
                return 0;
            else
                return 1;
        }

        private int PartagerProgression2()
        {
            var position = GetNextPosition();
            if (PlayerReviews[position].Trust == 70)
            {
                if (new Random().Next(100) < 65)
                    return 0;
                else
                    return 1;
            }
            else if (PlayerReviews[position].Trust > 70)
            {
                if (new Random().Next(100) < 75)
                    return 0;
                else
                    return 1;
            }
            else
            {
                if (new Random().Next(100) < 60)
                    return 0;
                else
                    return 1;
            }
        }

        private int PartagerProgression3()
        {
            var position = GetNextPosition();
            if (PlayerReviews[position].Trust >= 80)
            {
                if (new Random().Next(100) < 85)
                    return 0;
                else
                    return 1;
            }
            else if (PlayerReviews[position].Trust >= 70 && PlayerReviews[position].Trust <= 80)
            {
                if (new Random().Next(100) < 65)
                    return 0;
                else
                    return 1;
            }
            else if (PlayerReviews[position].Trust >= 60 && PlayerReviews[position].Trust <= 70)
            {
                if (new Random().Next(100) < 40)
                    return 0;
                else
                    return 1;
            }
            else
            {
                if (new Random().Next(100) < 20)
                    return 0;
                else
                    return 1;
            }
        }

        private int AvisVoisin()
        {
            int position = GetNextPosition();
            if (PlayerReviews[position].Trust < 60)
                return AvisVoisin1();
            if (PlayerReviews[position].Trust == 60)
                return AvisVoisin2();
            else
                return AvisVoisin3();
        }

        private int AvisVoisin1()
        {
            if (new Random().Next(100) < 30)
                return 1;
            else
                return 0;
        }

        private int AvisVoisin2()
        {
            if (new Random().Next(100) < 60)
                return 1;
            else
                return 0;
        }

        private int AvisVoisin3()
        {
            if (new Random().Next(100) < 80)
                return 1;
            else
                return 0;
        }

        private int OptionDemander(Choice choice)
        {
            if (PlayerData?.Player.Role.Team == Team.Inmate)
                return DemanderCriminal(choice);
            else
                return DemanderAssociate(choice);
        }

        private int DemanderCriminal(Choice choice)
        {
            var position = GetNextPosition();

            if (PlayerData?.Player.Progression >= 3 && PlayerReviews[position].Trust >= 80)
            {
                if (new Random().Next(100) < 90)
                    return 2;
                else
                    return 0;
            }

            return DemanderDefaultFunction();
        }

        private int DemanderAssociate(Choice choice)
        {
            if (PlayerData?.Player.Items.Any(x => x.Name == "Soap") == true)
            {
                if (new Random().Next(100) < 70 && choice.Answers.Count > 4)
                    return 4;
                else if (new Random().Next(100) < 20)
                    return 2;
                else if (new Random().Next(100) < 5)
                    return 0;
                else
                    return 1;
            }
            else
            {
                if (new Random().Next(100) < 50)
                    return 2;
                else if (new Random().Next(100) < 20)
                    return 0;
                else if (new Random().Next(100) < 15)
                    return 4;
                else
                    return 1;
            }
        }

        private int ChoixAction2()
        {
            if (new Random().Next(100) < 70 - 40 * BoardData?.Day / BoardData?.Names.Count)
                return 0;
            else
                return 1;
        }

        private int DemanderDefaultFunction()
        {
            if (new Random().Next(100) < 20)
                return 2;
            else if (new Random().Next(100) < 40)
                return 4;
            else if (new Random().Next(100) < 60)
                return 0;
            else if (new Random().Next(100) < 80)
                return 3;
            else
                return 1;
        }

        public override void SendPlayerMessage(string origin, string message)
        {

        }

        public override void SendBoardMessage(string message)
        {

        }

        public override void SendChoiceAnswer(int position, string name, Choice choice, int answer)
        {
            if (choice.Value.Contains("Est-ce que le guardien est la"))
            {
                PlayerAnswerGuardien[position] = answer == 0;
                PositionRepondeur = position;
            }
        }

        public override void Notify(BoardData boardData)
        {
            BoardData = boardData;

            if (PlayerReviews.Count == 0)
            {
                for (var i = 0; i < boardData.Names.Count; ++i)
                {
                    PlayerReviews[i] = new PlayerReview()
                    {
                        Trust = 60
                    };
                }
            }
        }

        public override void Notify(PlayerData playerData)
        {
            PlayerData = PlayerData;
        }

        private int GetNextPosition()
        {
            static int mod(int x, int m) => (x % m + m) % m;
            var position = mod((PlayerData?.Player.Position ?? 0) + (Direction == Direction.Right ? 1 : -1), BoardData?.Names.Count ?? 0);
            return position;
        }
    }
}
