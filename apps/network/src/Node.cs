using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Network
{
    public class Node(Socket socket)
    {
        public Socket Socket { get; } = socket;
        public static string EOM = "<|EOM|>";

        public static async Task<Node> Create(string host, int port, NodeType type)
        {
            var endpoint = await GetEndpoint(host, port);
            var socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            if (type == NodeType.Connect)
            {
                await socket.ConnectAsync(endpoint);
            }
            else
            {
                socket.Bind(endpoint);
            }

            return new Node(socket);
        }

        public void SendMessage(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message + EOM);
            Socket.Send(messageBytes, SocketFlags.None);
        }

        public string[] ReceiveMessage()
        {
            var buffer = new byte[1024];
            var received = Socket.Receive(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
            return response.Split(EOM);
        }

        public static async Task<IPEndPoint> GetEndpoint(string host, int port)
        {
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(host);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            return new(ipAddress, port);
        }

        public bool Connected()
        {
            return !(Socket.Poll(1000, SelectMode.SelectRead) && Socket.Available == 0) && Socket.Connected;
        }
    }
}