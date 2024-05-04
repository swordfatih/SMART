namespace Game
{
    public class Client : IObserver<BoardData>, IObserver<PlayerData>
    {
        public string Name { get; set; }

        public Client(string name)
        {
            Name = name;
        }

        public virtual int SendChoice(Choice choice)
        {
            return 0;
        }

        public virtual string AskInput(string instruction)
        {
            return "";
        }

        public virtual void SendPlayerMessage(string origin, string message)
        {

        }

        public virtual void SendBoardMessage(string message)
        {

        }

        public virtual void SendChoiceAnswer(int position, Choice choice, int answer)
        {
            
        }


        public virtual void Notify(BoardData value)
        {

        }

        public virtual void Notify(PlayerData value)
        {

        }
    }
}