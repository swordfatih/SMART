namespace Board
{
    public class Game(List<IClient> clients)
    {
        private readonly List<IClient> Clients = clients;
        public readonly List<Player> Players = [];
        public int GuardPosition { get; set; }
        public int? NextGuardPosition { get; set; } = null;

        public void Init()
        {
            var randomizer = new Random();
            var associate = randomizer.Next(0, Clients.Count);
            GuardPosition = randomizer.Next(0, Clients.Count);

            for (var i = 0; i < Clients.Count; ++i)
            {
                Players.Add(new(Clients[i], i, associate == i ? new AssociateRole() : new CriminalRole()));
            }
        }

        public void Run()
        {
            while (!HasEnded())
            {
                foreach (var player in Players)
                {
                    player.State.Clear();

                    IState state = new SafeState();

                    if(GuardPosition == player.Position)
                    {
                        state = new GuardState();   
                    }
                    else if(player.Status == Status.Confined)
                    {
                        state = new ConfinedState();
                    }

                    player.State.Push(GuardPosition == player.Position ? new GuardState() : new SafeState());
                    player.Status = Status.Alive;
                }

                Tour();

                GuardPosition = NextGuardPosition ?? AdjacentPlayer(GetPlayerByPosition(GuardPosition), Direction.Right).Position;
            }
        }

        public void Tour()
        {
            while (!StatesEmpty())
            {
                List<Action> actions = [];

                // récupérer les actions
                foreach (var player in Players)
                {
                    if (player.State.Count > 0)
                    {
                        var state = player.State.Pop();
                        var action = state.Action(this, player);
                        actions.Add(action);
                    }
                }

                // executer les actions
                foreach (var action in actions)
                {
                    action.Run(this);
                }
            }
        }

        public Player GetPlayerByPosition(int position)
        {
            return Players.Find(x => x.Position == position) ?? Players.First();
        }

        public List<Player> GetAlivePlayers(Player? except = null)
        {
            return new List<Player>(Players.Where(x => x.Status == Status.Alive && (except == null || x != except)));
        }

        private bool HasEnded()
        {
            return GetAlivePlayers().Find(x => x.Progression == 3) != null || GetAlivePlayers().Find(x => x.Role.Team == Team.Criminal) == null;
        }

        private bool StatesEmpty()
        {
            foreach (var player in Players)
            {
                if (player.State.Count > 0)
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

        override public string ToString()
        {
            var output = "";
            Players.ForEach(x => output += x + Environment.NewLine);
            return output;
        }
    }
}