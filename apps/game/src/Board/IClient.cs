namespace Game
{
    public interface IClient
    {
        public string Name { get; }

        public int AskChoice(Question question);
        public string AskInput(string instruction);
        public void SendMessage(string message);
    }
}