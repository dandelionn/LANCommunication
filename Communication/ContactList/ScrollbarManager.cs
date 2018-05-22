using System.Drawing;
using System.Windows.Forms;

namespace ContactList
{
    public class ScrollbarManager
    {
        LinkScrollbar.ContactListScrollbar _contactListScrollbar;
        FlowLayoutPanel _flowLayoutPanel;

        public ScrollbarManager(LinkScrollbar.ContactListScrollbar scrollbar, FlowLayoutPanel flowLayoutPanel)
        {
            _contactListScrollbar = scrollbar;
            _flowLayoutPanel = flowLayoutPanel;

            _contactListScrollbar.RegisterPanel(flowLayoutPanel);

            MakePanelLinkSettings();
            AdjustVScrollbarSettings();
        }


        public void MakePanelLinkSettings()
        {
            _flowLayoutPanel.MouseWheel += _contactListScrollbar.AdvancedScrollbar_MouseWheel;
            _flowLayoutPanel.AutoScroll = false;
            _flowLayoutPanel.HorizontalScroll.Maximum = 0;
            _flowLayoutPanel.VerticalScroll.Visible = true;
            _flowLayoutPanel.VerticalScroll.Enabled = true;

            _flowLayoutPanel.AutoScroll = true;
        }

        public void AdjustVScrollbarSettings()
        {
            _contactListScrollbar.Location = new Point(_flowLayoutPanel.Location.X + _flowLayoutPanel.Width - SystemInformation.VerticalScrollBarWidth, _flowLayoutPanel.Location.Y);
            _contactListScrollbar.Height = _flowLayoutPanel.Height;
            _contactListScrollbar.Width = SystemInformation.VerticalScrollBarWidth;
        }


        public void UpdateScrollBar()
        {
            int maximum = 0;
            foreach (ContactItem contactItem in _flowLayoutPanel.Controls)
            {
                maximum += contactItem.Height;
            }

            _contactListScrollbar.Minimum = 0;
            if (maximum > _flowLayoutPanel.Height)
            {
                _contactListScrollbar.Maximum = maximum - _flowLayoutPanel.Height;
            }

            _contactListScrollbar.Refresh();
        }

    }
}
