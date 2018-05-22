using System.Drawing;
using System.Windows.Forms;

namespace MessageBox
{
    public partial class MessageBox: UserControl
    {
        public MessageBox()
        {
            InitializeComponent();
            
            LayoutAdjuster.AdjustVScrollbarLocation(richTextBox, advancedVScrollbar);

            advancedVScrollbar.RegisterHandle(richTextBox.Handle);
        }

        public RichTextBox GetEditor()
        {
            return richTextBox;
        }

        private void richTextBox1_TextChanged(object sender, System.EventArgs e)
        {
            ScrollingManager.ScrollTextChanged(richTextBox, advancedVScrollbar);
        }

        private void richTextBox1_VScroll(object sender, System.EventArgs e)
        {
            ScrollingManager.ScrollVertically(richTextBox, advancedVScrollbar);
        }

        private void richTextBox_SizeChanged(object sender, System.EventArgs e)
        {
            ScrollingManager.ScrollSizeChanged(richTextBox, advancedVScrollbar);
        }
    }
}
