using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Network
{
    public class Node
    {
        public TcpClient Client { get; }
        public ConcurrentQueue<Packet> Packets { get; }

        public Node(TcpClient client)
        {
            Client = client;
            Packets = new ConcurrentQueue<Packet>();
        }

        public Node(string host, int port) : this(new TcpClient(host, port))
        {
        }

        public void Send(RequestType request, params string[] content)
        {
            Send(new Packet(request, content));
        }

        public void Send(Packet packet)
        {
            var bytes = Encoding.UTF8.GetBytes(packet.ToString());
            Client.GetStream().Write(bytes, 0, bytes.Length);
        }

        public IList<Packet> Receive()
        {
            var buffer = new byte[8192];
            var received = Client.GetStream().Read(buffer, 0, buffer.Length);
            var packets = Encoding.UTF8.GetString(buffer, 0, received).Split(new string[] { Packet.EOP }, System.StringSplitOptions.None);

            var results = new List<Packet>();
            foreach (var packet in packets)
            {
                if (packet.Length > 0)
                {
                    results.Add(Packet.FromString(packet));
                }
            }

            return results;
        }

        public bool Connected()
        {
            return !(Client.Client.Poll(1000, SelectMode.SelectRead) && Client.Available == 0) && Client.Connected;
        }
    }
}