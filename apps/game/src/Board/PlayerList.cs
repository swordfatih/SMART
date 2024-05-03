using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class PlayerList : IEnumerable<Player>
    {
        private List<Player> Players;

        public PlayerList(IEnumerable<Player> players)
        {
            Players = new(players);
        }

        public PlayerList()
        {
            Players = new();
        }

        public void Add(Player player)
        {
            Players.Add(player);
        }

        public Player? FindByClient(Client client)
        {
            return Players.Find(player => player.Client == client);
        }

        public PlayerList Only(Team team)
        {
            return new(Players.Where(x => x.Role.Team == team));
        }

        public PlayerList Only(Role role)
        {
            return new(Players.Where(x => x.Role == role));
        }

        public PlayerList Only(Status status)
        {
            return new(Players.Where(x => x.Status == status));
        }

        public PlayerList Except(Player except)
        {
            return new(Players.Where(x => x != except));
        }

        public PlayerList Except(Status status)
        {
            return new(Players.Where(x => x.Status != status));
        }

        public Player AdjacentPlayer(Player current, Direction direction)
        {
            return Players[AdjacentPosition(current.Position, direction)];
        }

        public Player AdjacentPlayer(int position, Direction direction)
        {
            return Players[AdjacentPosition(position, direction)];
        }

        public int AdjacentPosition(int position, Direction direction)
        {
            static int mod(int x, int m) => (x % m + m) % m;
            return mod(position + (direction == Direction.Right ? 1 : -1), Players.Count);
        }

        public Player? FindByPosition(int position)
        {
            return Players.Find(player => player.Position == position);
        }

        public Player? FindByName(string name)
        {
            return Players.Find(player => player.Client.Name == name);
        }

        public bool StatesEmpty()
        {
            return Players.All(player => player.States.Count == 0);
        }

        public IEnumerator<Player> GetEnumerator()
        {
            return Players.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}