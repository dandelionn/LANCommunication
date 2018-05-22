
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ContactList
{
    public partial class ContactList : UserControl
    {
        public delegate void ConnectionFunction(string ipaddress, bool connect);
        ConnectionFunction UpdateConnection;



        public void SetExternalConnectionFunction(ConnectionFunction function)
        {
            UpdateConnection = function;
        }


        private Dictionary<string, ContactItem> _items;
        ScrollbarManager _scrollbarManager;

        public ContactList()
        {
            InitializeComponent();
            _items = new Dictionary<string, ContactItem>();


            _scrollbarManager = new ScrollbarManager(contactListScrollbar, flowLayoutPanel);
            _scrollbarManager.UpdateScrollBar();
        }


        private int _connectionsCount = 0;
        private int _connectionsLimit = 1;


        private string _defaultButtonText = ButtonText.Connect;
        private bool myVar = true;

        public bool IsClient
        {
            get { return myVar; }
            set
            {
                if(value == true)
                {
                    _defaultButtonText = ButtonText.Connect;
 
                }
                else
                {
                    _defaultButtonText = ButtonText.Listen;
                }

                myVar = value;
            }
        }

        public void ResetButtonText(string address)
        {
            ContactItem contactItem = _items[address];
            contactItem.SetButtonText(_defaultButtonText);
            _connectionsCount = 0;
        }

        private void ButtonConnectDisconnect_Click(object sender, EventArgs e)
        {
            if (IsClient == true)
            {
                Button button = sender as Button;
                ContactItem contactItem = button.Parent as ContactItem;
                if (button.Text == ButtonText.Disconnect)
                {
                    button.Text = ButtonText.Connect;
                    _connectionsCount--;

                    UpdateConnection?.Invoke(contactItem.GetAddress(), false);
                }
                else
                {
                    if (_connectionsCount < _connectionsLimit)
                    {
                        button.Text = ButtonText.Disconnect;
                        _connectionsCount++;

                        UpdateConnection?.Invoke(contactItem.GetAddress(), true);
                    }
                }
            }
            else
            {
                Button button = sender as Button;
                ContactItem contactItem = button.Parent as ContactItem;
                if (button.Text == ButtonText.Disconnect)
                {
                    button.Text = ButtonText.Listen;
                    _connectionsCount--;

                    UpdateConnection?.Invoke(contactItem.GetAddress(), false);
                }
                else
                {
                    if (_connectionsCount < _connectionsLimit)
                    {
                        button.Text = ButtonText.Disconnect;
                        _connectionsCount++;

                        UpdateConnection?.Invoke(contactItem.GetAddress(), true);
                    }
                }
            }
        }

        public void Add(string nickname, string address, string status)
        {
            ContactItem contactItem = new ContactItem(nickname, address, status);
            _items.Add(address, contactItem);

            contactItem.SetButtonText(_defaultButtonText);
            SetEvents(contactItem);
            flowLayoutPanel.Controls.Add(contactItem);

            _scrollbarManager.UpdateScrollBar();
        }

        public void RemoveByAddress(string address)
        {
            ContactItem contactItem = _items[address];
            _items.Remove(address);

            RemoveEvents(contactItem);
            flowLayoutPanel.Controls.Remove(contactItem);

            _scrollbarManager.UpdateScrollBar();
        }

        public void Add(ContactItem contactItem)
        {
            _items.Add(contactItem.GetAddress(), contactItem);

            SetEvents(contactItem);
            contactItem.SetButtonText(_defaultButtonText);

            flowLayoutPanel.Controls.Add(contactItem);

            _scrollbarManager.UpdateScrollBar();
        }

        public void Remove(ContactItem contactItem)
        {
            _items.Remove(contactItem.GetAddress());

            RemoveEvents(contactItem);
            flowLayoutPanel.Controls.Remove(contactItem);

            _scrollbarManager.UpdateScrollBar();
        }

        private void SetEvents(ContactItem contactItem)
        {
            contactItem.MouseWheel += contactListScrollbar.AdvancedScrollbar_MouseWheel;
            foreach (Control control in contactItem.Controls)
            {
                control.MouseWheel += contactListScrollbar.AdvancedScrollbar_MouseWheel;

                if (control is Button)
                {
                    control.Click += ButtonConnectDisconnect_Click;
                }
            }

        }

        private void RemoveEvents(ContactItem contactItem)
        {
            contactItem.MouseWheel -= contactListScrollbar.AdvancedScrollbar_MouseWheel;
            foreach (Control control in contactItem.Controls)
            {
                control.MouseWheel -= contactListScrollbar.AdvancedScrollbar_MouseWheel;

                if (control is Button)
                {
                    control.Click -= ButtonConnectDisconnect_Click;
                }
            }
        }
    }
}
