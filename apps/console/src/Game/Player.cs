namespace Player
{
    public class Player(string name, int position, Role role)
    {
        public string Name { get; } = name;
        public Role Role { get; } = role;
        public int Position { get; } = position;
        public Stack<IState> State { get; set; } = [];
        public int Progression { get; set; }
        public List<Item> Items { get; set; } = [];

        public override string ToString()
        {
            return Name + " (" + Role + ")";
        }
    }
}