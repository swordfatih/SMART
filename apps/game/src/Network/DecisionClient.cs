using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Game;
using Network;
using System.Diagnostics.Contracts;
namespace Interface
{
    public class PlayerReview{
        public int Trust{
            get;set;
        }
    }

    public class DecisionClient : Client
    {   public Dictionary<int,bool> playerAnswerGuardien;
        public int positionRepondeur;
        public BoardData? BoardData { get; set; }

        public PlayerData? PlayerData { get; set; }
        public Dictionary<int, PlayerReview> PlayerReviews;
        public Direction Direction {get; set;}
        public DecisionClient() : base(Guid.NewGuid().ToString())
        {
            PlayerReviews = new ();
        }

        public override int SendChoice(Choice question)
        {
            var instruction = question.Value.ToString();
            if (instruction == "Que vouliez-vous communiquez ?"){
                if (instruction.Contains("You received a message from(Accept, Propagate"))
                {   return AccepterPropager();

                }
                else if (instruction.Contains("Vers quel joueur rediriger le gardien"))
                {
                    //Celui qui notre trust pour lui est le plus grand
                    return RedirigerGuardien();
                }
                else if (instruction.Contains("Choisissez l'action du jour"))
                {
                    return ChoixAction();
                }
                else if (instruction.Contains("Que voulez-vous demander ? (Gardien, Opinion, Progression, Message, Donation)"))
                {
                    if (question.Answers.Count == 5) { };
                    return OptionDemander();
                }
                else if (instruction.Contains("Communiquer avec le voisin de (Gauche, Droite)"))
                {
                    return CommuniquerVoisin();
                }
                else if (instruction.Contains("Le gardien est-il devant toi ? (Oui, Non)"))
                {
                    return GuardienDevant();
                }
                else if (instruction.Contains("Veux-tu partager ta progression ? (Oui, Non)"))
                {
                    return PartagerProgression();
                }
                else if (instruction.Contains("Ton avis sur ton autre voisin ? (Méchant, Gentil, Je ne sais pas)"))
                {
                    return AvisVoisin();
                }
                else if (instruction.Contains("Contre qui vous voulez voter ? (joueurs)"))
                {
                    return Voter();

                }
                else if (instruction.Contains("Voulez-vous recevoir un objet (oui,non, faire tourner) ?"))
                {
                    return RecevoirObjet();
                }

                else{return 0;}
            }
            else{return 0;}
        
        }

        
        private int AccepterPropager(){
            if(new Random().Next(100) < 50){
                return 0;
            }
            else{
                return 1;
            }
        
        }
        
        private int RedirigerGuardien(){
            //Seulement pour les mechants : va rediriger vers celui qu'on est le plus sur qu'il est gentil donc le plus grand Trust
            int trust = 60;
            for(int i=0 ; i < PlayerReviews.Count;i++){

                if(PlayerReviews[i].Trust > trust){
                    trust = PlayerReviews[i].Trust;
                }
                
        //rediriger le guardien vers position receptrice
                return i; 
            }
           return 0;
        }
        
        public int AttributionTrust(){
            for(int i=0; i < BoardData.Names.Count;i++){

                PlayerReviews[i].Trust= 60;
            }   
            
            if(playerAnswerGuardien[positionRepondeur]){
                if(PlayerData.HasGuard){
                    PlayerReviews[positionRepondeur].Trust += 10;
                }
                else{
                    PlayerReviews[positionRepondeur].Trust -= 20;
                }
            }
            return 0;
        }
       
        private int CommuniquerVoisin(){
            if(new Random().Next(100) < 50){
                return 0;
            }
            else{
                return 1;
            }
        }

        private int Voter(){
            int Trust = 60;
            if(PlayerData.Player.Role.Team==Team.Inmate){
                //Chercher le joueur avec le plus petit Trust
                for(int i=0; i < BoardData.Names.Count ;i++){
                    if(PlayerReviews[i].Trust < Trust){
                    Trust = PlayerReviews[i].Trust;
                    }

                }
                //voter pour celui qui a un trust == Trust
                for(int j=0; j <BoardData.Names.Count; j++){
                   if(Trust==PlayerReviews[j].Trust){
                   return j;
                   } 
                }         
                return 0;
            }
            else{
                //Chercher le joueur avec le trust le plus grand
                for(int i=0; i < BoardData.Names.Count ;i++){
                    if(PlayerReviews[i].Trust > Trust){
                    Trust = PlayerReviews[i].Trust;
                    }
                }

                //voter pour celui qui a le Trust le plus grand
                for(int j=0; j <BoardData.Names.Count; j++){
                   if(Trust==PlayerReviews[j].Trust){
                   return j;
                   } 
                }
                return 0;
            }
        
        }

        private int ChoixAction()
        {
            if (PlayerData?.Player.Role.Team == Team.Inmate)
            {

                if (PlayerData.HasGuard == true)
                {
                    return 0;
                }
                else
                {
                    if (PlayerData.Player.Progression >= 3)
                    {
                        return 1;
                    }
                    else
                    {
                        return ChoixAction2();
                    }
                }
            }
            else{
                if (PlayerData.HasGuard == true){
                        //rediriger le guardien vers suspect le plus eleve 
                    return RedirigerGuardien();
                }
                else{
                    
                    if (PlayerData.Player.Progression < 2){
                        if (new Random().Next(100) < 50){
                            return 0;
                        }
                        else { return 1;}
                    }

                    else{
                        if (new Random().Next(100) < 90){
                            return 1;
                        }
                        else { return 0; }
                    }
                    
                }  

            }

        }
        private int RecevoirObjet()
        {
            var position = GetNextPosition();
            if (PlayerReviews[position].Trust < 60)
            {

                return RecevoirObjet1();

            }
            else
            {
                return RecevoirObjet2();
            }

        }

        private int RecevoirObjet1()
        {
            if (new Random().Next(100) < 30)
            {
                return 0;
            }
            else
            {
                if (new Random().Next(100) < 50)
                {
                    return 1;
                }
                else { return 2; }
            }
        }
        private int RecevoirObjet2()
        {
            if (new Random().Next(100) < 50)
            {
                return 0;
            }
            else
            {
                if (new Random().Next(100) < 50)
                {
                    return 1;
                }
                else { return 2; }
            }
        }
        private int PartagerProgression(){

            if (PlayerData.Player.Progression <= 1)
            {
                return PartagerProgression1();
            }
            else if (PlayerData.Player.Progression == 2)
            {
                return PartagerProgression2();
            }
            else if (PlayerData.Player.Progression == 3)
            {
                return PartagerProgression3();
            }
            else{
                return 1;
            }
        }

        private int PartagerProgression1()
        {
            if (new Random().Next(100) < 80)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private int PartagerProgression2()
        {
            var position =GetNextPosition();
            if (PlayerReviews[position].Trust == 70)
            {
                if (new Random().Next(100) < 65)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else if (PlayerReviews[position].Trust > 70)
            {
                if (new Random().Next(100) < 75)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else{
                if (new Random().Next(100) < 60)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

        }
        private int PartagerProgression3()
        {

            var position = GetNextPosition();
            if (PlayerReviews[position].Trust >= 80)
            {
                if (new Random().Next(100) < 85)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }

            }
            else if (PlayerReviews[position].Trust>=70 && PlayerReviews[position].Trust <= 80){
                if (new Random().Next(100) < 65)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }

            }
            else if (PlayerReviews[position].Trust >=60 && PlayerReviews[position].Trust <= 70){
                if (new Random().Next(100) < 40)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }

            }

            else{
                if (new Random().Next(100) < 20)
                {
                    return 0;
                }
                else { return 1; }
            }

        }
        private int GuardienDevant()
        {
            if (PlayerData?.Player.Role.Team == Team.Inmate)
            {
                if (new Random().Next(100) < 90)
                {
                    if (PlayerData.HasGuard == true)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (PlayerData.HasGuard == true)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

                }
            }
            else
            {
                if (PlayerData.HasGuard == true)
                {
                    return 1;
                }
                else
                {
                    if (new Random().Next(100) < 45)
                    {
                        return 1;
                    }
                    else { return 0; }
                }
            }


        }

        private int AvisVoisin(){   
            
            int position=GetNextPosition();
            if (PlayerReviews[position].Trust < 60){
                return AvisVoisin1();
            }
            if (PlayerReviews[position].Trust == 60){
                return AvisVoisin2();
            }
            else{
                return AvisVoisin3();
            }
            
        }

        private int AvisVoisin1()
        {
            if (new Random().Next(100) < 30)
            {
/*                if (Avis == mechant)
                {
                    return 0;
                }
                if (Avis == gentil)
                {
                    return 1;
                }
                if (Avis == ne sais pas){
                    return 2;
                }
*/            
                return 1;
            }
            
            else
            {
/*              if (Avis == gentil){
                    return 0;
                }
                if (Avis == mechant){
                    return 1;
                }
                if (Avis == ne sais pas){
                    if (new Random().Next(100) < 50)
                    {
                        return 0;
                    }
                    else { return 1; }
                }
  */              
                return 0;
            }
        }
        private int AvisVoisin2()
        {
            if (new Random().Next(100) < 60)
            {
/*                if (Avis == mechant)
                {
                    return 0;
                }
                if (Avis == gentil)
                {
                    return 1;
                }
                if (Avis == ne sais pas){
                    return 2;
                }
*/
                return 1;
            
            }

            else
            {
/*                if (Avis == gentil)
                {
                    return 0;
                }
                if (Avis == mechant)
                {
                    return 1;
                }
                if (Avis == ne sais pas){
                    if (new Random().Next(100) < 50)
                    {
                        return 0;
                    }
                    else { return 1; }
                }
*/            
                return 0;            
            }
        }
        private int AvisVoisin3(){
            if (new Random().Next(100) < 80)
            {
/*              if (Avis == mechant)
                {
                    return 0;
                }
                if (Avis == gentil)
                {
                    return 1;
                }
                if (Avis == ne sais pas){
                    return 2;
                }
*/            
                return 1;
            }

            else
            {
/*                if (Avis == gentil)
                {
                    return 0;
                }
                if (Avis == mechant)
                {
                    return 1;
                }
                if (Avis == ne sais pas){
                    if (new Random().Next(100) < 50)
                    {
                        return 0;
                    }
                    else { return 1; }
                }
*/            
                return 0;
            }
        }
        private int OptionDemander()
        {

            if (PlayerData?.Player.Role.Team == Team.Inmate)
            {
                return DemanderCriminal();
            }

            else
            {
                return DemanderAssociate();
            }
        }

        private int GetNextPosition(){

            static int mod(int x, int m) => (x % m + m) % m;
            var position = mod((PlayerData?.Player.Position ?? 0) + (Direction == Direction.Right ? 1 : -1), BoardData?.Names.Count ?? 0);
            
            return position;
        }


        private int DemanderCriminal()
        {
           var position = GetNextPosition();

            if (PlayerData.Player.Progression >= 3 && PlayerReviews[position].Trust >= 80)
            {
                if (new Random().Next(100) < 90)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }

            return DemanderDefaultFunction();
        }
        private int DemanderAssociate() {   
            if (PlayerData.Player.Items.Any(x => x.Name == "Soap")){
                
                if (new Random().Next(100) < 70)
                {
                    return 4;
                }
                else if (new Random().Next(100) < 20)
                {
                    return 2;
                }
                else if (new Random().Next(100) < 5)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }

            }
            else{
                
                if (new Random().Next(100) < 50){
                    return 2;
                }

                else if (new Random().Next(100) < 20)
                {
                    return 0;
                }
                else if (new Random().Next(100) < 15)
                {
                    return 4;
                }
                else{
                    return 1;
                }

            }

        }
        private int ChoixAction2()
        {
            if (new Random().Next(100) < 70 - 40 * BoardData.Day /BoardData.Names.Count){
                return 0;
            }
            else{
                return 1;
            }
        }
    

        public int DemanderDefaultFunction()
        {
            
            if (new Random().Next(100) < 20){
                return 2;
            }
            else if (new Random().Next(100) < 40){
                return 4;
            }
            else if (new Random().Next(100) < 60){
                return 0;
            }
            else if (new Random().Next(100) < 80){
                return 3;
            }
            else{
                return 1;
            }
        }
        

        public override void SendPlayerMessage(string origin, string message)
        {
            
        }

        public override void SendBoardMessage(string message)
        {

        }

        public override void SendChoiceAnswer(int position, string name, Choice choice, int answer)
        {  
            if(choice.Value.Contains("Est-ce que le guardien est la")){
                playerAnswerGuardien[position]= answer == 0;
                positionRepondeur = position;
            }
        }
        
    
        public override void Notify(BoardData boardData)
        {
            BoardData = boardData;

            if(PlayerReviews.Count == 0){
                for(var i = 0; i < boardData.Names.Count; ++i){
                    PlayerReviews[i] = new(){
                        Trust = 60
                    };

                }
            }
        }

        public override void Notify(PlayerData playerData)
        {
            PlayerData = playerData;
        }
    }
}
