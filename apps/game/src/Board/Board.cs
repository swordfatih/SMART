using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Game
{
    public class Board : IObservable<BoardData>
    {
        public List<IObserver<BoardData>> Observers;
        public PlayerList Players;
        public StreamWriter Logger { get; set; }
        public Dictionary<Player, int> Votes;
        public int GuardPosition { get; set; }
        public int? NextGuardPosition { get; set; } = null;
        public int Day { get; set; } = 0;
        public static int SHOWER_RATE = 4;

        public Board(Stream logger)
        {
            Players = new();
            Observers = new();
            Votes = new();
            Logger = new(logger) { AutoFlush = true };
        }

        public void Init(List<Client> clients)
        {
            var randomizer = new Random();
            GuardPosition = randomizer.Next(0, clients.Count);

            var associate_count = Math.Ceiling((double)clients.Count / 4);
            var associates = clients.OrderBy(x => randomizer.Next()).Take((int)associate_count).ToList();

            var soap = randomizer.Next(0, clients.Count);
            var poison = randomizer.Next(0, associates.Count);

            for (var i = 0; i < clients.Count; ++i)
            {
                var player = new Player(clients[i], i, associates.Contains(clients[i]) ? new AssociateRole() : new InmateRole());
                Players.Add(player);

                if (i == soap)
                {
                    player.Items.Add(new SoapItem());
                }

                if (associates.IndexOf(clients[i]) == poison)
                {
                    player.Items.Add(new PoisonItem());
                }

                Logger.WriteLine(player.ToString());
                Subscribe(player.Client);
            }
        }

        public void Run()
        {
            while (GetWinner() == null)
            {
                Day++;
                Logger.WriteLine($"day:{Day}");

                foreach (var player in Players.Except(Status.Dead).Except(Status.Escaped))
                {
                    // new state
                    player.States.Clear();
                    if (Day % SHOWER_RATE == 0)
                    {
                        player.States.Push(new ShowerState());
                    }
                    else if (player.Status == Status.Confined)
                    {
                        player.States.Push(new ConfinedState());
                    }
                    else if (GuardPosition == player.Position)
                    {
                        player.States.Push(new GuardState());
                    }
                    else if (player.Status == Status.Alive)
                    {
                        player.States.Push(new SafeState());
                    }

                    // new status
                    if (player.Progression == Player.MAX_PROGRESSION && Day % SHOWER_RATE != 0)
                    {
                        player.Escaping = true;
                    }

                    player.Status = Status.Alive;
                }

                Notify();

                Tour();
                Notify();

                UpdateGuard();
                UpdateEscapes();
                Notify();
            }
        }

        public void Tour()
        {
            while (!Players.StatesEmpty())
            {
                var states = new List<(Task<Action>, Player)>();
                var actions = new List<Action>();

                // récupérer les states
                foreach (var player in Players.Except(Status.Dead))
                {
                    if (player.States.Count > 0)
                    {
                        var state = player.States.Pop();
                        player.CurrentState = () => state.Action(this, player);
                        states.Add((Task.Run(player.CurrentState), player));
                    }
                }

                Votes.Clear();

                // executer les states
                foreach (var state in states)
                {
                    try
                    {
                        state.Item1.Wait();
                        var result = state.Item1.Result;
                        actions.Add(result);
                    }
                    catch (Exception)
                    {
                        var current = Task.Run(state.Item2.CurrentState);
                        current.Wait();
                        var result = current.Result;
                        actions.Add(result);
                    }
                }

                // traitement des poisons
                foreach (var player in Players.Except(Status.Dead).Only(Team.Inmate))
                {
                    if (player.Items.Any(x => x is PoisonItem))
                    {
                        actions.Add(new UsePoisonAction(player));
                    }
                }

                // executer les actions
                foreach (var action in actions)
                {
                    action.Run(this);
                    Logger.WriteLine(action.ToString());
                }

                // traitement des votes
                if (Day % SHOWER_RATE == 0)
                {
                    var max = Votes.Values.Max();
                    var players = Votes.Where(x => x.Value == max).Select(x => x.Key).ToList();

                    if (players.Count == 1 && max > 0)
                    {
                        var action = new DieAction(players.First());
                        action.Run(this);

                        Logger.WriteLine(action.ToString());
                    }
                }
            }
        }

        public void UpdateEscapes()
        {
            Players.Where(x => x.Escaping).ForEach(x => x.Progression++);
        }

        public void UpdateGuard()
        {
            GuardPosition = NextGuardPosition ?? Players.Only(Status.Alive).AdjacentPlayer(GuardPosition, Direction.Right)?.Position ?? -1;
            NextGuardPosition = null;

            foreach (var player in Players.Except(Team.Associate).Except(Status.Dead).Except(Status.Escaped))
            {
                if (player.Position == GuardPosition && player.HasDug)
                {
                    var action = new DieAction(player);
                    action.Run(this);

                    Logger.WriteLine(action.ToString());
                }

                player.HasDug = false;
            }

            Logger.WriteLine($"guard:{GuardPosition}");
        }

        public Team? GetWinner()
        {
            var inmates = Players.Only(Team.Inmate);
            var associates = Players.Only(Team.Associate);
            var alive_inmates = inmates.Except(Status.Dead);
            var alive_associates = associates.Except(Status.Dead);
            var escaped_inmates = inmates.Only(Status.Escaped);

            if (escaped_inmates.Count() > alive_inmates.Count() || alive_associates.Count() == 0)
            {
                return Team.Inmate;
            }

            if (alive_associates.Count() > alive_inmates.Count())
            {
                return Team.Associate;
            }

            return null;
        }

        public void Subscribe(IObserver<BoardData> observer)
        {
            Observers.Add(observer);
        }

        public void Notify(IObserver<BoardData> observer)
        {
            observer.Notify(new BoardData(
                new List<string>(Players.Except(Status.Dead).Select(x => x.Client.Name)),
                Day
            ));
        }

        public void Notify()
        {
            Observers.ForEach(Notify);
            NotifyPlayers();
        }

        public void NotifyPlayers()
        {
            foreach (var player in Players)
            {
                player.Notify(player.Client, this);
            }
        }

        override public string ToString()
        {
            var output = "";

            foreach (var player in Players)
            {
                output += player.ToString() + Environment.NewLine;
            }

            return output;
        }
    }
}