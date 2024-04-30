using System.Collections.Generic;

namespace Game
{
    public enum Status
    {
        Alive,
        Dead,
        Confined,
    }

    public class Player : IObservable<PlayerData>
    {
        private readonly List<IObserver<PlayerData>> Observers;
        public Client Client { get; }
        public Role Role { get; }
        public int Position { get; }
        public Stack<IState> States { get; set; }
        public Status Status { get; set; } = Status.Alive;
        public int Progression { get; set; }
        public List<Item> Items { get; set; }

        public Player(Client client, int position, Role role)
        {
            Client = client;
            Position = position;
            Role = role;
            Items = new();
            Observers = new();
            States = new();
        }

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
            return $"{Client.Name}:{Role}";
        }
    }
}