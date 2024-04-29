namespace Game
{
    public abstract class Client(string name) : IObserver<BoardData>, IObserver<PlayerData>
    {
        public string Name { get; } = name;
        public abstract int AskChoice(Question question);
        public abstract string AskInput(string instruction);
        public abstract void SendMessage(string message);
        public abstract void Notify(BoardData value);
        public abstract void Notify(PlayerData value);
    }
}