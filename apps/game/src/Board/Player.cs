using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
        public Client Client { get; set; }
        public Role Role { get; set; }
        public int Position { get; set; }
        public Stack<State> States { get; set; }
        public Status Status { get; set; } = Status.Alive;
        public int Progression { get; set; }
        public List<Item> Items { get; set; }
        private Func<Action>? CurrentAction;
        private CancellationTokenSource Source;

        public Player(Client client, int position, Role role)
        {
            Client = client;
            Position = position;
            Role = role;
            Items = new();
            Observers = new();
            States = new();
            Source = new();
        }

        public void Update(Board board)
        {
            Observers.ForEach(x => x.Notify(new PlayerData(this, board.GuardPosition == Position)));
        }

        public void Subscribe(IObserver<PlayerData> observer)
        {
            Observers.Add(observer);
        }

        public void SetCurrentAction(Func<Action> action)
        {
            CurrentAction = action;
        }

        public Func<Action>? GetCurrentAction()
        {
            return CurrentAction;
        }

        public void RestartAction()
        {
            Source.Cancel();
            Task.Run(CurrentAction, GetCancelToken());
        }

        public CancellationToken GetCancelToken()
        {
            return Source.Token;
        }

        public override string ToString()
        {
            return $"{Client.Name}:{Role}";
        }
    }
}