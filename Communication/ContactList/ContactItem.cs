
using System.Windows.Forms;

namespace ContactList
{
    public partial class ContactItem : UserControl
    {
        public ContactItem()
        {
            InitializeComponent();
        }

        public ContactItem(string nickname, string address, string status)
        {
            InitializeComponent();

            labelNickname.Text = nickname;
            labelAddress.Text = address;
            labelStatus.Text = status;
        }

        public void SetName(string nickname)
        {
            labelNickname.Text = nickname;
        }

        public void SetAddress(string address)
        {
            labelAddress.Text = address;
        }

        public void SetStatus(string status)
        {
            labelStatus.Text = status;
        }

        public void SetButtonText(string text)
        {
            buttonConnect.Text = text;
        }

        public string GetName()
        {
            return labelNickname.Text;
        }

        public string GetAddress()
        {
            return labelAddress.Text;
        }

        public string GetStatus()
        {
            return labelStatus.Text;
        }

        public string ButtonText()
        {
            return buttonConnect.Text;
        }
    }
}
