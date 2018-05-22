using System.Threading;
using System.Windows.Forms;

namespace TestAppServer
{
    public partial class ServerForm : Form
    {
        Communication.Server server;

        public void CreateAddressList()
        {
            foreach (string address in Communication.IPAddresses.GetAddressesOnLAN())
            {
                contactList.Add(Resources.NoName, address, Resources.StatusUndefined);
            }
        }

        public ServerForm()
        {
            InitializeComponent();

            contactList.IsClient = false;

            CreateAddressList();

            chatBox.SetMessageSender(SendMessage);
            contactList.SetExternalConnectionFunction(UpdateConnection);
        }

        private void UpdateConnection(string ipaddress, bool connect)
        {
            if (connect == true)
            {
                server = new Communication.Server(1030, UpdateMessageBox, ResetButtonText, ResetServer);

                Thread thread = new Thread(x => server.StartListening(ipaddress));
                thread.Start();
            }
            else
            {
                SendMessage(Resources.ConnectionCloseFlag);

                server.CloseConnections();
                server = null;
            }
        }

        private bool SendMessage(string text)
        {
            if (server != null)
            {
                if (server.Sockets.Count > 0)
                {
                    if (server.Sockets[0].Connected)
                    {
                        if (text != string.Empty)
                        {
                            server.SendData(text, server.Sockets[0]);

                            return true;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(Resources.PartnerNotConnected);
                    }
                }
            }

            return false;
        }

        private void ResetServer()
        {
            server = null;
        }

        delegate void SetTextCallback(string text);
        private void UpdateMessageBox(string data)
        {
            if (this.chatBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateMessageBox);
                this.Invoke(d, new object[] { data });
            }
            else
            {
                chatBox.PostMessage(data, Resources.RemoteUserColor, Resources.RemoteUserName);
            }
        }

        private void ResetButtonText(string address)
        {
            if (this.contactList.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(ResetButtonText);
                this.Invoke(d, new object[] { address });
            }
            else
            {
                contactList.ResetButtonText(address);
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
            {
                server.CloseConnections();
            }
        }
    }
}
