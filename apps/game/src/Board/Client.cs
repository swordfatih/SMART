namespace Game
{
    public class Client : IObserver<BoardData>, IObserver<PlayerData>
    {
        public string Name { get; set; }

        public Client(string name)
        {
            Name = name;
        }

        public virtual int AskChoice(Question question)
        {
            return 0;
        }

        public virtual string AskInput(string instruction)
        {
            return "";
        }

        public virtual void SendMessage(string message)
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