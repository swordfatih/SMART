using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Interface
{
    public class Client
    {
        public string Name { get; }
        public Socket Socket { get; }

        private Client(string name, Socket socket)
        {
            Name = name;
            Socket = socket;
        }

        public static async Task<Client> Create(string name)
        {
            var endpoint = await getEndpoint("192.168.1.39", 11000);

            Socket socket = new(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            await socket.ConnectAsync(endpoint);

            await SendMessage(socket, name);

            return new Client(name, socket);
        }

        public static async Task SendMessage(Socket socket, string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message + "<|EOM|>");
            _ = await socket.SendAsync(messageBytes, SocketFlags.None);
            Console.WriteLine($"Socket client sent message: \"{message}\"");

            // Receive ack.
            var buffer = new byte[1024];
            var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);

            if (response == "<|ACK|>")
            {
                Console.WriteLine($"Socket client received acknowledgment: \"{response}\"");
            }
        }

        private static async Task<IPEndPoint> getEndpoint(string host, int port)
        {
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(host);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            return new(ipAddress, port);
        }
    }
}