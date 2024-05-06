using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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

        public PlayerList Except(int position)
        {
            return new(Players.Where(x => x.Position != position));
        }

        public PlayerList Except(Player? except)
        {
            return new(Players.Where(x => x != except));
        }

        public PlayerList Except(Team? except)
        {
            return new(Players.Where(x => x.Role.Team != except));
        }

        public PlayerList Except(Status status)
        {
            return new(Players.Where(x => x.Status != status));
        }

        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        public Player? AdjacentPlayer(Player current, Direction direction)
        {
            var players = new List<Player>(Players);

            if(!players.Contains(current))
            {
                players.Add(current);
            }

            if (players.Count <= 1)
            {
                return null;
            }

            var sorted = players.OrderBy(player => player.Position).ToList();
            var index = sorted.FindIndex(player => player.Position == current?.Position);
            var next = Mod(index + (int)direction, sorted.Count);
            return sorted[next];
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