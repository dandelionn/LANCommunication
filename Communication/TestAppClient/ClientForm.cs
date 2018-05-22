using System.Threading;
using System.Windows.Forms;

namespace TestAppClient
{
    public partial class ClientForm : Form
    {
        Communication.Client client;

        public void CreateAddressList()
        {
            foreach(string address in Communication.IPAddresses.GetAddressesOnLAN())
            {
                contactList.Add(Resources.NoName, address, Resources.StatusUndefined);
            }
        }

        public ClientForm()
        {
            InitializeComponent();


            CreateAddressList();

            chatBox.SetMessageSender(SendMessage);
            contactList.SetExternalConnectionFunction(UpdateConnection);
        }

        private void UpdateConnection(string ipaddress, bool connect)
        {
            if (connect == true)
            {
                client = new Communication.Client(1030, ipaddress, UpdateMessageBox, ResetButtonText, ResetClient);

                Thread clientMainThread = new Thread(x => client.StartClient());
                clientMainThread.Start();
            }
            else
            {
                SendMessage(Resources.ConnectionCloseFlag);

                client.CloseConnection();
                ResetClient();
            }
        }

        private bool SendMessage(string text)
        {
            if (client != null)
            {
                if (client.isConnected())
                {
                    if (text != string.Empty)
                    {
                        client.SendData(Resources.MessageFlag + text);

                        return true;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Resources.PartnerNotConnected);
                }
            }

            return false;
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

        private void ResetClient()
        {
            client = null;
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                client.CloseConnection();
            }
        }
    }
}
