using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game
{
    public enum Status
    {
        Alive,
        Dead,
        Confined,
        Escaped
    }

    public class Player : IObservable<PlayerData>
    {
        public static int MAX_PROGRESSION = 3;
        private readonly List<IObserver<PlayerData>> Observers;
        public Client Client { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Auto)]
        public Role Role { get; set; }
        public int Position { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.All, ItemTypeNameHandling = TypeNameHandling.All)]
        public Stack<State> States { get; set; }
        public Status Status { get; set; } = Status.Alive;
        public int Progression { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Auto, ItemTypeNameHandling = TypeNameHandling.Auto)]
        public List<Item> Items { get; set; }
        [JsonIgnore]
        public Func<Action>? CurrentState;
        public bool HasDug { get; set; } = false;

        public Player(Client client, int position, Role role)
        {
            Position = position;
            Role = role;
            Items = new();
            Observers = new();
            States = new();
            Client = client;
            Subscribe(Client);
        }

        public void Notify(IObserver<PlayerData> observer, Board board)
        {
            observer.Notify(new PlayerData(this, board.GuardPosition == Position));
        }

        public void Subscribe(IObserver<PlayerData> observer)
        {
            Observers.Add(observer);
        }

        public void SetClient(Client client)
        {
            Observers.Remove(Client);
            Client = client;
            Subscribe(client);
        }

        public override string ToString()
        {
            return $"{Client.Name}:{Role}";
        }
    }
}