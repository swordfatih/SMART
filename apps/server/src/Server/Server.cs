using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Interface
{
    public class Server
    {
        public Socket Socket { get; }
        public List<NetworkClient> Clients { get; } = [];

        private Server(Socket socket)
        {
            Socket = socket;
        }

        public static async Task<Server> Create()
        {
            var endpoint = await getEndpoint("192.168.1.39", 11000);

            Socket socket = new(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endpoint);

            return new Server(socket);
        }

        public async Task Run()
        {
            Socket.Listen(100);

            while (true)
            {
                Console.WriteLine("Waiting for a new connection...");
                var handler = await Socket.AcceptAsync();
                Console.WriteLine("Received a new connection");
                
                // Receive message.
                var buffer = new byte[1024];
                var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);

                Console.WriteLine(response);

                var eom = "<|EOM|>";
                if (response.IndexOf(eom) > -1)
                {
                    var name = response.Replace(eom, "");
                    Console.WriteLine($"Socket server received message: \"{name}\"");

                    var ackMessage = "<|ACK|>";
                    var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
                    await handler.SendAsync(echoBytes, 0);
                    Console.WriteLine($"Socket server sent acknowledgment: \"{ackMessage}\"");

                    Clients.Add(new NetworkClient(name, handler));
                }
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