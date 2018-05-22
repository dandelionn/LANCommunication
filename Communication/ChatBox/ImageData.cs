using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBox
{
    public class ImageData
    {
        public string ImageTag { get; set; }
        public int SelectionStart { get; set; }
        public int SelectionLength { get; set; }

        public ImageData(string imageTag, int selectionStart, int selectionLength)
        {
            ImageTag = imageTag;
            SelectionStart = selectionStart;
            SelectionLength = selectionLength;
        }
    }
}
