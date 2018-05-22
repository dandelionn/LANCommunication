using System.Drawing;
using System.Windows.Forms;


namespace MessageBox
{
    public static class LayoutAdjuster
    {
        public static void AdjustRichTextBoxSize(RichTextBox richTextBox, ChatBoxScrollbar.ChatBoxScrollbar advancedVScrollbar)
        {
            richTextBox.Width -= advancedVScrollbar.Width - SystemInformation.VerticalScrollBarWidth;
        }

        public static void AdjustVScrollbarLocation(RichTextBox richTextBox, ChatBoxScrollbar.ChatBoxScrollbar advancedVScrollbar)
        {
            advancedVScrollbar.Location = new Point(richTextBox.Location.X + richTextBox.Width - SystemInformation.VerticalScrollBarWidth, richTextBox.Location.Y);
            advancedVScrollbar.Height = richTextBox.Height;
            advancedVScrollbar.Width = SystemInformation.VerticalScrollBarWidth;
        }
    }
}
