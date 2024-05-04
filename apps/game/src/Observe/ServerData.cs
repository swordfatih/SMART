using System.Collections.Generic;

namespace Game
{
    public class ServerData
    {
        public List<string> Clients { get; set; }
        public List<string> Bots { get; set; }
        public bool Running { get; set; }

        public ServerData(IEnumerable<string> clients, IEnumerable<string> bots, bool running)
        {
            Clients = new(clients);
            Bots = new(bots);
            Running = running;
        }
    }
}