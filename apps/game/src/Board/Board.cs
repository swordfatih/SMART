using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game
{
    public class Board : IObservable<BoardData>
    {
        private readonly List<IObserver<BoardData>> Observers;
        private readonly List<Client> Clients;
        public readonly List<Player> Players;
        public StreamWriter Logger { get; set; }
        public int[]? Votes;
        public int GuardPosition { get; set; }
        public int? NextGuardPosition { get; set; } = null;
        public int Day { get; set; } = 0;
        public readonly static int SHOWER_RATE = 2;

        public Board(List<Client> clients, Stream logger)
        {
            Clients = clients;
            Players = new();
            Observers = new();
            Logger = new(logger);
        }

        public void Init()
        {
            var randomizer = new Random();
            var associate = randomizer.Next(0, Clients.Count);
            GuardPosition = randomizer.Next(0, Clients.Count);

            for (var i = 0; i < Clients.Count; ++i)
            {
                var player = new Player(Clients[i], i, associate == i ? new AssociateRole() : new CriminalRole());
                Players.Add(player);

                Logger.WriteLine(player.ToString());
            }
        }

        public void Run()
        {
            while (!HasEnded())
            {
                Day++;

                foreach (var player in Players)
                {
                    player.States.Clear();

                    IState state = new SafeState();

                    if (player.Status == Status.Dead)
                    {
                        continue;
                    }
                    else if (Day % SHOWER_RATE == 0)
                    {
                        state = new ShowerState();
                    }
                    else if (GuardPosition == player.Position)
                    {
                        state = new GuardState();
                    }
                    else if (player.Status == Status.Confined)
                    {
                        state = new ConfinedState();
                    }

                    player.States.Push(state);
                    player.Status = Status.Alive;
                }

                Logger.WriteLine($"day:{Day}");

                // Notify subscribers
                Observers.ForEach(x => x.Notify(new BoardData(
                    new List<string>(GetAlivePlayers().Select(x => x.Client.Name)),
                    Day
                )));

                Players.ForEach(x => x.Update(this));

                // Run day
                Tour();

                // Update guard position
                GuardPosition = NextGuardPosition ?? AdjacentPlayer(GetPlayerByPosition(GuardPosition), Direction.Right).Position;
                NextGuardPosition = null;
            }
        }

        public void Tour()
        {
            while (!StatesEmpty())
            {
                var actions = new List<Action>();

                // récupérer les actions
                foreach (var player in GetAlivePlayers())
                {
                    if (player.States.Count > 0)
                    {
                        var state = player.States.Pop();
                        var action = state.Action(this, player);
                        actions.Add(action);
                    }
                }

                Votes = new int[GetAlivePlayers().Count];

                // executer les actions
                foreach (var action in actions)
                {
                    action.Run(this);

                    Logger.WriteLine(action.ToString());
                }

                // traitement des votes
                if (Day % 4 == 0)
                {
                    var max = Votes.Max();
                    var indices = Votes.Select((x, i) => new { Index = i, Value = x }).Where(x => x.Value == max).Select(x => x.Index).ToList();

                    if (indices.Count == 1 && max > 0)
                    {
                        var player = Players[indices[0]];
                        var action = new DieAction(player);
                        action.Run(this);

                        Logger.WriteLine(action.ToString());
                    }
                }
            }
        }

        public Player GetPlayerByPosition(int position)
        {
            return Players.Find(x => x.Position == position) ?? Players.First();
        }

        public List<Player> GetAlivePlayers(Player? except = null)
        {
            return new List<Player>(Players.Where(x => x.Status != Status.Dead && (except == null || x != except)));
        }

        private bool HasEnded()
        {
            return GetAlivePlayers().Find(x => x.Progression == 3) != null || GetAlivePlayers().Find(x => x.Role.Team == Team.Criminal) == null;
        }

        private bool StatesEmpty()
        {
            foreach (var player in Players)
            {
                if (player.States.Count > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public Player AdjacentPlayer(Player current, Direction direction)
        {
            static int mod(int x, int m) => (x % m + m) % m;

            foreach (var player in Players)
            {
                if (player.Position == mod(current.Position + (direction == Direction.Right ? 1 : -1), Players.Count))
                {
                    return player;
                }
            }

            return current;
        }

        public void Subscribe(IObserver<BoardData> observer)
        {
            Observers.Add(observer);
        }

        override public string ToString()
        {
            var output = "";
            Players.ForEach(x => output += x + Environment.NewLine);
            return output;
        }
    }
}