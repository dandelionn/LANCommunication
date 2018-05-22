using System.Net;
using System.Net.Sockets;

namespace Communication
{
    public class ServerParameters
    {
        public int Port { get; set; }
        public IPHostEntry ipHostInfo { get; set; }
        public IPAddress ipAddress { get; set; }
        public IPEndPoint localEndPoint { get; set; }
        public Socket listener { get; set; }

        public ServerParameters(int port)
        {
            SetParameters(port);
        }

        public void SetParameters(int port)
        {
            Port = port;
            ipHostInfo = Dns.Resolve(Dns.GetHostName());

            foreach (IPAddress address in ipHostInfo.AddressList)
            {
                if (address.ToString().StartsWith("192."))
                {
                    ipAddress = address;
                    break;
                }
            }

            localEndPoint = new IPEndPoint(ipAddress, Port);
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}
