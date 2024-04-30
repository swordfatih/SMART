using System.Collections.Generic;

namespace Game
{
    public enum Status
    {
        Alive,
        Dead,
        Confined,
    }

    public class Player(Client client, int position, Role role) : IObservable<PlayerData>
    {
        private readonly List<IObserver<PlayerData>> Observers = [];
        public Client Client { get; } = client;
        public Role Role { get; } = role;
        public int Position { get; } = position;
        public Stack<IState> State { get; set; } = [];
        public Status Status { get; set; } = Status.Alive;
        public int Progression { get; set; }
        public List<Item> Items { get; set; } = [];

        public void Update(Board board)
        {
            Observers.ForEach(x => x.Notify(new PlayerData(this, board.GuardPosition == Position)));
        }

        public void Subscribe(IObserver<PlayerData> observer)
        {
            Observers.Add(observer);
        }

        public override string ToString()
        {
            return $"{Client} ({Role})";
        }
    }
}