using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Communication
{
    public class Server : ServerParameters
    {
        string _passPhrase = "123vvfd34%%f3232";

        private static Mutex mutex = new Mutex();
        public delegate void UpdateObject(string data);
        UpdateObject Function;

        public delegate void ResetServer();
        ResetServer ResetServerFunction;

        public delegate void ResetConnectionButtonState(string address);
        ResetConnectionButtonState ResetButtonFunction;

        public List<Socket> Sockets;

        public Server(int port, UpdateObject function, ResetConnectionButtonState resetButtonFunction, ResetServer resetServerFunction) : base(port)
        {
            Sockets = new List<Socket>();

            Function = function;
            ResetButtonFunction = resetButtonFunction;
            ResetServerFunction = resetServerFunction;
        }


        public void SendData(string data, Socket socket)
        {
            string encrypted = Cryptography.Encrypt.EncryptString(data, _passPhrase);
            byte[] msg = Encoding.ASCII.GetBytes(encrypted + Resources.EOF);

            int bytesSent = socket.Send(msg);

            if (bytesSent == 0)
            {
                SendData(data, socket);
            }
        }

        public void CloseConnection(Socket socket)
        {
            SendData(Resources.ConnectionCloseFlag, socket); //send close_connection signal to the client
            socket.Shutdown(SocketShutdown.Both);
            socket.Disconnect(false);
        }



        public void StartListening(string address) 
        {
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(1);

                //while (true)
                //{
                    Socket handler = listener.Accept();

                    IPEndPoint remoteIpEndPoint = handler.RemoteEndPoint as IPEndPoint;

                    if (remoteIpEndPoint.Address.ToString() != address)
                    {
                        CloseConnection(handler);
                    }
                    else
                    {
                        Sockets.Add(handler);

                        Thread thread = new Thread(x => DoWork(handler));
                        thread.Start();
                    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.Read();
        }

        public void DoWork(Socket handler)
        {
            try
            {
                while (handler.Connected == true)
                {
                    byte[] bytes = new byte[1024];
                    string data = null;

                    while (handler.Connected == true)
                    {
                        bytes = new byte[1024];

                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf(Resources.EOF) > -1)
                        {
                            break;
                        }
                    }

                    data = data.Replace(Resources.EOF, "");

                    string decrypted = Cryptography.Encrypt.DecryptString(data, _passPhrase);

                    if (decrypted.Contains(Resources.ConnectionCloseFlag))
                    {
                        IPEndPoint remoteIpEndPoint = handler.RemoteEndPoint as IPEndPoint;

                        UpdateButtonState(remoteIpEndPoint.Address.ToString());
                        ResetServerOutside();
                        CloseConnections();

                        break;
                    }
                    else
                    {
                        decrypted = decrypted.Replace(Resources.MessageFlag, "");
                        UpdateObjectOnMainThread(decrypted);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void CloseConnections()
        {
            foreach (Socket socket in Sockets)
            {
                if (socket != null && socket.Connected == true)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(false);
                    socket.Close();
                    socket.Dispose();
                }
            }

            listener.Close();
            listener.Dispose();

            Sockets.Clear();
        }

        public void ResetServerOutside()
        {
            mutex.WaitOne();
            ResetServerFunction.Invoke();
            mutex.ReleaseMutex();
        }
        public void UpdateButtonState(string address)
        {
            mutex.WaitOne();
            ResetButtonFunction.Invoke(address);
            mutex.ReleaseMutex();
        }

        public void UpdateObjectOnMainThread(string data)
        {
            mutex.WaitOne();
            Function.Invoke(data);
            mutex.ReleaseMutex();
        }
    }
}
