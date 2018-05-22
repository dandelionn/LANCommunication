using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MessageBox
{
    public enum SBOrientation : int
    {
        SB_HORZ = 0x0,
        SB_VERT = 0x1,
        SB_CTL = 0x2,
        SB_BOTH = 0x3
    }
    public enum ScrollBarType : uint
    {
        SbHorz = 0,
        SbVert = 1,
        SbCtl = 2,
        SbBoth = 3
    }
    public enum Message : uint
    {
        WM_VSCROLL = 0x0115
    }
    public enum ScrollBarCommands : uint
    {
        SB_THUMBPOSITION = 4
    }
    [Serializable, StructLayout(LayoutKind.Sequential)]
    struct SCROLLINFO
    {
        public uint cbSize;
        public uint fMask;
        public int nMin;
        public int nMax;
        public uint nPage;
        public int nPos;
        public int nTrackPos;
    }
    enum SCROLLINFOFLAGS
    {
        SIF_RANGE = 1,
        SIF_PAGE = 2,
        SIF_POS = 4,
        SIF_DISABLENOSCROLL = 8,
        SIF_TRACKPOS = 16,
        SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS
    }

    public static class ScrollingManager
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);
        [DllImport("user32.dll")]
        static extern int SetScrollInfo(IntPtr hwnd, int fnBar, [In] ref SCROLLINFO lpsi, bool fRedraw);
        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public static int PreviousRichTextBoxHeight = 0;

        public static void ScrollTextChanged(RichTextBox richTextBox, ChatBoxScrollbar.ChatBoxScrollbar advancedVScrollbar)
        {
            SCROLLINFO scrollinfo = GetScrollInfo(richTextBox);

            if (scrollinfo.nMax > advancedVScrollbar.Maximum + richTextBox.Height)
            {
                PreviousRichTextBoxHeight = richTextBox.Height;
                advancedVScrollbar.Maximum = scrollinfo.nMax - richTextBox.Height;
                advancedVScrollbar.ThumbPosition = advancedVScrollbar.Maximum;
                SetScrollingInfo(richTextBox.Handle, advancedVScrollbar.Maximum);
            }
        }

        private static void SetScrollingInfo(IntPtr handle, int value)
        {
            int nPos = value;
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(handle, (uint)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
        }

        public static void ScrollVertically(RichTextBox richTextBox, ChatBoxScrollbar.ChatBoxScrollbar advancedVScrollbar)
        {
            SCROLLINFO scrollinfo = GetScrollInfo(richTextBox);

            if (advancedVScrollbar.ThumbPosition != scrollinfo.nTrackPos)
            {
                if (scrollinfo.nMax > advancedVScrollbar.Maximum + richTextBox.Height)
                {
                    PreviousRichTextBoxHeight = richTextBox.Height;
                    advancedVScrollbar.Maximum = scrollinfo.nMax - richTextBox.Height;
                }

                advancedVScrollbar.ThumbPosition = scrollinfo.nTrackPos;
            }
        }

        private static SCROLLINFO GetScrollInfo(RichTextBox richTextBox)
        {
            SCROLLINFO scrollinfo = new SCROLLINFO();
            scrollinfo.cbSize = (uint)Marshal.SizeOf(typeof(SCROLLINFO));
            scrollinfo.fMask = (uint)Convert.ToInt32(SCROLLINFOFLAGS.SIF_ALL);
            GetScrollInfo(richTextBox.Handle, (int)ScrollBarType.SbVert, ref scrollinfo);

            return scrollinfo;
        }


        public static void ScrollSizeChanged(RichTextBox richTextBox, ChatBoxScrollbar.ChatBoxScrollbar advancedVScrollbar)
        {
           ///a litle bug here, have a look a it later, in certain situations trackbar freezes
            if (advancedVScrollbar.Maximum >= 0)
            {              
                SCROLLINFO scrollinfo = GetScrollInfo(richTextBox);
                if (scrollinfo.nMax - richTextBox.Height > 0 )
                {
                    richTextBox.ScrollToCaret();
                    advancedVScrollbar.Maximum = scrollinfo.nMax - richTextBox.Height;

                    if (scrollinfo.nTrackPos <= advancedVScrollbar.Maximum)
                    {
                        advancedVScrollbar.ThumbPosition = scrollinfo.nTrackPos;
                    }
                    else
                    {
                        advancedVScrollbar.ThumbPosition = advancedVScrollbar.Maximum;
                    }
                }
            }
        }
    }
}
