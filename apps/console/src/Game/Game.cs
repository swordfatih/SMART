namespace Game
{
    public class Game(List<string> players)
    {
        private readonly List<string> Names = players;
        public readonly List<Player.Player> Players = [];
        public int GuardPosition { get; set; }
        public int NextGuardPosition { get; set; }

        public void Init()
        {
            var randomizer = new Random();
            var associate = randomizer.Next(0, Names.Count);
            var guard = randomizer.Next(0, Names.Count);

            for (int i = 0; i < Names.Count; ++i)
            {
                var player = new Player.Player(Names[i], i, associate == i ? new Player.AssociateRole() : new Player.CriminalRole());
                player.State.Push(i == guard ? new Player.GuardState() : new Player.SafeState());
                Players.Add(player);
            }

            NextGuardPosition = guard;
        }

        public void Run()
        {
            GuardPosition = NextGuardPosition;

            while (!StatesEmpty())
            {
                List<Player.Action> actions = [];

                // récupérer les actions
                foreach (var player in Players)
                {
                    if (player.State.Count > 0)
                    {
                        Console.WriteLine("-- tour de " + player + " --");
                        var state = player.State.Pop();
                        var action = state.Action(player);
                        actions.Add(action);
                    }
                }

                // executer les actions
                foreach (var action in actions)
                {
                    action.Run(this);
                    Console.WriteLine(action);
                }
            }
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

        public Player.Player AdjacentPlayer(Player.Player current, Player.Direction direction)
        {
            static int mod(int x, int m) => (x % m + m) % m;

            foreach (var player in Players)
            {
                if (player.Position == mod(current.Position + (direction == Player.Direction.Right ? 1 : -1), Players.Count))
                {
                    return player;
                }
            }

            return current;
        }

        override public string ToString()
        {
            var output = "";

            for (var i = 0; i < Players.Count; ++i)
            {
                output += i + ". " + Players[i].Name + Environment.NewLine;
            }

            return output;
        }
    }
}