using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatBox
{
    public class RTBImageBuilder
    {
        [DllImport("gdiplus.dll")]
        private static extern uint GdipEmfToWmfBits(IntPtr _hEmf,
                                                  uint _bufferSize, byte[] _buffer,
                                                  int _mappingMode, EmfToWmfBitsFlags _flags);

        public Control _control;
        public Image _image;

        public RTBImageBuilder(Control control, Image image)
        {
            _control = control;
            _image = image; 
        }
   
        public StringBuilder GenerateRtf()
        {
            return EmbedImage(_image);
        }

        private bool IsImage(string filename)
        {
            var valids = new[] { ".jpeg", ".jpg", ".png", ".ico", ".gif", ".bmp", ".emp", ".wmf", ".tiff" };

            return valids.Contains(Path.GetExtension(filename));
        }

        private StringBuilder EmbedImage(Image img)
        {
            StringBuilder rtf = new StringBuilder();

            // Append the RTF header
            rtf.Append(@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033");
            // Create the font table using the RichTextBox's current font and append
            // it to the RTF string
            rtf.Append(GetFontTable(_control.Font));
            // Create the image control string and append it to the RTF string
            rtf.Append(GetImagePrefix(img));
            // Create the Windows Metafile and append its bytes in HEX format
            rtf.Append(getRtfImage(img));
            // Close the RTF image control string
            rtf.Append(@"}");

            return rtf;
        }

        private enum EmfToWmfBitsFlags
        {
            EmfToWmfBitsFlagsDefault = 0x00000000,
            EmfToWmfBitsFlagsEmbedEmf = 0x00000001,
            EmfToWmfBitsFlagsIncludePlaceable = 0x00000002,
            EmfToWmfBitsFlagsNoXORClip = 0x00000004
        };

        private struct RtfFontFamilyDef
        {
            public const string Unknown = @"\fnil";
            public const string Roman = @"\froman";
            public const string Swiss = @"\fswiss";
            public const string Modern = @"\fmodern";
            public const string Script = @"\fscript";
            public const string Decor = @"\fdecor";
            public const string Technical = @"\ftech";
            public const string BiDirect = @"\fbidi";
        }
    
        private string GetFontTable(Font font)
        {
            var fontTable = new StringBuilder();
            // Append table control string
            fontTable.Append(@"{\fonttbl{\f0");
            fontTable.Append(@"\");
            var rtfFontFamily = new HybridDictionary();
            rtfFontFamily.Add(FontFamily.GenericMonospace.Name, RtfFontFamilyDef.Modern);
            rtfFontFamily.Add(FontFamily.GenericSansSerif, RtfFontFamilyDef.Swiss);
            rtfFontFamily.Add(FontFamily.GenericSerif, RtfFontFamilyDef.Roman);
            rtfFontFamily.Add("UNKNOWN", RtfFontFamilyDef.Unknown);

            // If the font's family corresponds to an RTF family, append the
            // RTF family name, else, append the RTF for unknown font family.
            fontTable.Append(rtfFontFamily.Contains(font.FontFamily.Name) ? rtfFontFamily[font.FontFamily.Name] : rtfFontFamily["UNKNOWN"]);
            // \fcharset specifies the character set of a font in the font table.
            // 0 is for ANSI.
            fontTable.Append(@"\fcharset0 ");
            // Append the name of the font
            fontTable.Append(font.Name);
            // Close control string
            fontTable.Append(@";}}");
            return fontTable.ToString();
        }

        private string GetImagePrefix(Image _image)
        {
            float xDpi, yDpi;
            var rtf = new StringBuilder();
            using (Graphics graphics = _control.CreateGraphics())
            {
                xDpi = graphics.DpiX;
                yDpi = graphics.DpiY;
            }
           
            var picw = (int)Math.Round((_image.Width / xDpi) * 2540);  
            var pich = (int)Math.Round((_image.Height / yDpi) * 2540);    
            var picwgoal = (int)Math.Round((_image.Width / xDpi) * 1440);
            var pichgoal = (int)Math.Round((_image.Height / yDpi) * 1440);

            // Append values to RTF string
            rtf.Append(@"{\pict\wmetafile8");
            rtf.Append(@"\picw");
            rtf.Append(picw);
            rtf.Append(@"\pich");
            rtf.Append(pich);
            rtf.Append(@"\picwgoal");
            rtf.Append(picwgoal);
            rtf.Append(@"\pichgoal");
            rtf.Append(pichgoal);
            rtf.Append(" ");

            return rtf.ToString();
        }

        private string getRtfImage(Image image)
        {
            // Used to store the enhanced metafile
            MemoryStream stream = null;
            // Used to create the metafile and draw the image
            Graphics graphics = null;
            // The enhanced metafile
            Metafile metaFile = null;
            try
            {
                var rtf = new StringBuilder();
                stream = new MemoryStream();
                // Get a graphics context from the RichTextBox
                using (graphics = _control.CreateGraphics())
                {
                    // Get the device context from the graphics context
                    IntPtr hdc = graphics.GetHdc();
                    // Create a new Enhanced Metafile from the device context
                    metaFile = new Metafile(stream, hdc);
                    // Release the device context
                    graphics.ReleaseHdc(hdc);
                }

                // Get a graphics context from the Enhanced Metafile
                using (graphics = Graphics.FromImage(metaFile))
                {
                    // Draw the image on the Enhanced Metafile
                    graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
                }

                // Get the handle of the Enhanced Metafile
                IntPtr hEmf = metaFile.GetHenhmetafile();
                // A call to EmfToWmfBits with a null buffer return the size of the
                // buffer need to store the WMF bits.  Use this to get the buffer
                // size.
                uint bufferSize = GdipEmfToWmfBits(hEmf, 0, null, 8, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
                // Create an array to hold the bits
                var buffer = new byte[bufferSize];
                // A call to EmfToWmfBits with a valid buffer copies the bits into the
                // buffer an returns the number of bits in the WMF.  
                uint _convertedSize = GdipEmfToWmfBits(hEmf, bufferSize, buffer, 8, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
                // Append the bits to the RTF string
                foreach (byte t in buffer)
                {
                    rtf.Append(String.Format("{0:X2}", t));
                }
                return rtf.ToString();
            }
            finally
            {
                if (graphics != null)
                    graphics.Dispose();
                if (metaFile != null)
                    metaFile.Dispose();
                if (stream != null)
                    stream.Close();
            }
        }
    }
}
