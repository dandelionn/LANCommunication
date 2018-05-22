using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatBox
{
    public class ContentModifier
    {
        ImagePainter _imagePainter;
        List<ImageData> _imageDataSet;
        RichTextBox _messageBox;
        RichTextBox _editMessageBox;
        List<string> _imageTags;
        List<string> _profileImageColors; 

        public ContentModifier(RichTextBox messageBox, RichTextBox editMessageBox)
        {
            _messageBox = messageBox;
            _editMessageBox = editMessageBox;

            _imagePainter = new ImagePainter(_messageBox);
            _imageDataSet = new List<ImageData>();

            _imageTags = new List<string>() { ":)", ":(", ":l", ":d", ":D", ":|", ":?" };
            _profileImageColors = new List<string>() { "0xFFFF0000", "0xFF00FF00", "0xFF0000FF", "0xFFFFFF00", "0xFFFFA500" };
        }

        private void ShowImages(int textLength)
        {
            int nrOfAddedElements = 0;

            CountItems(_imageTags, ref nrOfAddedElements, textLength);
            CountItems(_profileImageColors, ref nrOfAddedElements, textLength);

            _imageDataSet.Sort((y, x) => (x.SelectionStart.CompareTo(y.SelectionStart)));
            _imagePainter.ApplyImageRtfs(_messageBox, _imageDataSet, nrOfAddedElements);
        }

        public void CountItems(List<string> items, ref int nrOfAddedElements, int textLength)
        {
            foreach (string item in items)
            {
                int index = _messageBox.TextLength - textLength;

                while ((index = _messageBox.Text.IndexOf(item, index)) != -1)
                {
                    _imageDataSet.Add(new ImageData(item, index, item.Length));
                    index += item.Length;
                    nrOfAddedElements++;
                }
            }
        }

        public void PostMessage(string editedText, string userColor, string userType)
        {
            if (editedText != String.Empty)
            {
                string profileImageColor = userColor + " " + userType + ": ";
                string newLine = "\n";

                _messageBox.Text += profileImageColor + editedText + newLine;

                int textLength = profileImageColor.Length + editedText.Length + newLine.Length;

                ShowImages(textLength);

                _messageBox.SelectionStart = _messageBox.Text.Length;
            }
        }
    }
}
