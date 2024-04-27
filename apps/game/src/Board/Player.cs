namespace Game
{
    public enum Status
    {
        Alive,
        Dead,
        Confined,
    }

    public class Player(IClient client, int position, Role role)
    {
        public IClient Client { get; } = client;
        public Role Role { get; } = role;
        public int Position { get; } = position;
        public Stack<IState> State { get; set; } = [];
        public Status Status { get; set; } = Status.Alive;
        public int Progression { get; set; }
        public List<Item> Items { get; set; } = [];

        public override string ToString()
        {
            return Client + " (" + Role + ")";
        }
    }
}