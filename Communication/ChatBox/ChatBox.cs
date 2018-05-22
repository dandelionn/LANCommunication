using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ChatBox
{
    public partial class ChatBox: UserControl
    {
        Mutex mutex = new Mutex();

        public delegate bool PostMessageExternal(string data);

        public Mutex GetMutex()
        {
            return mutex;
        }

        public void SetMessageSender(PostMessageExternal function)
        {
            PostMessageOut = function;
        }

        PostMessageExternal PostMessageOut;

        ContentModifier _contentModifier;

        public string LocalUserColor = "0xFF0000FF";
        public string LocalUserName = "Local User";

        public ChatBox()
        {
            InitializeComponent();

            _contentModifier = new ContentModifier(messageBox.GetEditor(), editMessageBox);       
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if(PostMessageOut?.Invoke(editMessageBox.Text) == true)
            {
                PostMessage(editMessageBox.Text, LocalUserColor, LocalUserName);

                editMessageBox.Text = String.Empty;
            }
        }

        private void editMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                PostMessageOut?.Invoke(editMessageBox.Text);
                PostMessage(editMessageBox.Text, LocalUserColor, LocalUserName);

                editMessageBox.Text = String.Empty;
            }
        }

        public void PostMessage(string editedText, string userColor, string userType)
        {
            //mutex.WaitOne();

            _contentModifier.PostMessage(editedText, userColor, userType);

            //mutex.ReleaseMutex();
        }
    }
}
