using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Communication
{
    public class Client : ClientParameters
    {
        string _passPhrase = "123vvfd34%%f3232";

        private static Mutex mutex = new Mutex();
        public delegate void UpdateObject(string data);
        UpdateObject Function;

        public delegate void ResetConnectionButtonState(string address);
        ResetConnectionButtonState ResetButtonFunction;
        public delegate void ResetClient();
        ResetClient ResetClientFunction;

        public Client(int port, string ip, UpdateObject function, ResetConnectionButtonState resetConnectionButton, ResetClient resetClientFunction) : base(port, ip)
        {
            Function = function;
            ResetButtonFunction = resetConnectionButton;
            ResetClientFunction = resetClientFunction;
        }

        public void StartClient()
        {
            bool exception = false;
            try
            {
                sender.Connect(remoteEP);

                Thread thread = new Thread(x => DoWork(sender));
                thread.Start(); 
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show(Resources.ArgumentNullException + ane.ToString());
                exception = true;
            }
            catch (SocketException se)
            {
                MessageBox.Show(Resources.SocketException + se.ToString());
                exception = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(Resources.UnexpectedException + e.ToString());
                exception = true;
            }

            if(exception == true)
            {
                UpdateButtonState(remoteEP.Address.ToString());
                ResetClientOutside();
                CloseConnection();
            }
        }



        public void SendData(string data)
        {
            string encrypted = Cryptography.Encrypt.EncryptString(data, _passPhrase);
            byte[] msg = Encoding.ASCII.GetBytes(encrypted + Resources.EOF);

            int bytesSent = sender.Send(msg);

            if (bytesSent == 0)
            {
                SendData(data);
            }
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
                        CloseConnection();

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

        public void CloseConnection()
        {
            if (sender != null && sender.Connected == true)
            {
                sender.Shutdown(SocketShutdown.Both);
                sender.Disconnect(false);
                sender.Close();
                sender.Dispose();
            }
        }

        public void ResetClientOutside()
        {
            mutex.WaitOne();
            ResetClientFunction?.Invoke();
            mutex.ReleaseMutex();
        }

        public void UpdateButtonState(string address)
        {
            mutex.WaitOne();
            ResetButtonFunction?.Invoke(address);
            mutex.ReleaseMutex();
        }

        public void UpdateObjectOnMainThread(string data)
        {
            mutex.WaitOne();
            Function?.Invoke(data);
            mutex.ReleaseMutex();
        }

        public bool isConnected()
        {
            return sender.Connected;
        }
    }
}
