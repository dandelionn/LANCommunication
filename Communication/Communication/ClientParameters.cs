using System.Net;
using System.Net.Sockets;

namespace Communication
{
    public class ClientParameters
    {
        public int Port { get; set; }
        public IPHostEntry ipHostInfo { get; set; }
        public IPAddress ipAddress { get; set; }
        public IPEndPoint remoteEP { get; set; }
        public Socket sender { get; set; }

        public ClientParameters(int port, string ip)
        {
            SetParameters(port, ip);
        }

        public void SetParameters(int port, string ip)
        {
            Port = port;
            ipHostInfo = Dns.Resolve(ip);
            ipAddress = ipHostInfo.AddressList[0];
            remoteEP = new IPEndPoint(ipAddress, Port);
            sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}
