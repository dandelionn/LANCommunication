using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace ChatBox
{
    public class ImagePainter
    { 
        Dictionary<string, StringBuilder> rtfs;

        public ImagePainter(RichTextBox richTextBox)
        {
            rtfs = new Dictionary<string, StringBuilder>();
            RegisterProfileImages(richTextBox);
            RegisterEmotionImages(richTextBox);
        }

        public ImagePainter(RichTextBox richTextBox, List<string> profileImageColors)
        {
            rtfs = new Dictionary<string, StringBuilder>();
            RegisterProfileImages(richTextBox, profileImageColors);
            RegisterEmotionImages(richTextBox);
        }

        private Image CreateImage(int argb)
        {
            Image resultImage = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
            using (Graphics grp = Graphics.FromImage(resultImage))
            {
                SolidBrush color = new SolidBrush(Color.FromArgb(argb));
                grp.FillRectangle(color, 0, 0, resultImage.Width, resultImage.Height);
            }

            return resultImage;
        }

        public void RegisterProfileImages(RichTextBox richTextBox)
        {
            List<string> profileImageColors = new List<string>() { "0xFFFF0000", "0xFF00FF00", "0xFF0000FF", "0xFFFFFF00", "0xFFFFA500" };
            RTBImageBuilder imageBuilder;
            foreach (string color in profileImageColors)
            {
                imageBuilder = new RTBImageBuilder(richTextBox, CreateImage(int.Parse(color.Remove(0,2), NumberStyles.AllowHexSpecifier)));
                rtfs[color] = imageBuilder.GenerateRtf();
            }
        }

        public void RegisterProfileImages(RichTextBox richTextBox, List<string> profileImageColors)
        {
            RTBImageBuilder imageBuilder;
            foreach (string color in profileImageColors)
            {
                imageBuilder = new RTBImageBuilder(richTextBox, CreateImage(int.Parse(color)));
                rtfs[color] = imageBuilder.GenerateRtf();
            }
        }

        private void RegisterEmotionImages(RichTextBox richTextBox)
        {
            List<string> imageTags = new List<string>() {":)", ":(", ":l", ":d", ":D", ":|", ":?"};

            foreach(string imageTag in imageTags)
            {
                RegisterEmotionImage(richTextBox, imageTag);
            }
        }

        private void RegisterEmotionImage(RichTextBox richTextBox, string imageTag)
        {
            RTBImageBuilder imageBuilder;

            switch (imageTag)
            {
                case ":)":
                    {
                        imageBuilder = new RTBImageBuilder(richTextBox, EmotionImage24x24.Smily);
                        rtfs[":)"] = imageBuilder.GenerateRtf();
                    }
                    break;

                case ":l":
                    {
                        imageBuilder = new RTBImageBuilder(richTextBox, EmotionImage24x24.Laughing);
                        rtfs[":l"] = imageBuilder.GenerateRtf();
                    }
                    break;

                case ":?":
                    {
                        imageBuilder = new RTBImageBuilder(richTextBox, EmotionImage24x24.Questioning);
                        rtfs[":?"] = imageBuilder.GenerateRtf();
                    }
                    break;

                case ":D":
                    {
                        imageBuilder = new RTBImageBuilder(richTextBox, EmotionImage24x24.Excited);
                        rtfs[":D"] = imageBuilder.GenerateRtf();
                    }
                    break;

                case ":(":
                    {
                        imageBuilder = new RTBImageBuilder(richTextBox, EmotionImage24x24.Sad);
                        rtfs[":("] = imageBuilder.GenerateRtf();
                    }
                    break;

                case ":|":
                    {
                        imageBuilder = new RTBImageBuilder(richTextBox, EmotionImage24x24.Confuzed);
                        rtfs[":|"] = imageBuilder.GenerateRtf();
                    }
                    break;
                case ":d":
                    {
                        imageBuilder = new RTBImageBuilder(richTextBox, EmotionImage24x24.Joyful);
                        rtfs[":d"] = imageBuilder.GenerateRtf();
                    }
                    break;
            }
        }

        public void ApplyImageRtfs(RichTextBox richTextBox, List<ImageData> imageDataSet, int nrOfAddedElements)
        {
            ApplyImageRtfs(richTextBox, imageDataSet);
            AdjustImageDataSet(imageDataSet, nrOfAddedElements);
        }

        private void ApplyImageRtfs(RichTextBox richTextBox, List<ImageData> imageDataSet)
        {
            foreach (ImageData imageData in imageDataSet)
            {
                richTextBox.SelectionStart = imageData.SelectionStart;
                richTextBox.SelectionLength = imageData.SelectionLength;
                richTextBox.SelectedRtf = rtfs[imageData.ImageTag].ToString();
            }
        }

        private void AdjustImageDataSet(List<ImageData> imageDataSet, int nrOfAddedElements)
        {
            if (nrOfAddedElements > 0)
            {
                int nrOfRemovedPositions = 0;
                int index = 0;

                for (index = nrOfAddedElements - 1; index > 0; index--)
                {
                    imageDataSet[index].SelectionStart -= nrOfRemovedPositions;
                    nrOfRemovedPositions += imageDataSet[index].SelectionLength - 1;
                    imageDataSet[index].SelectionLength = 1;
                }

                imageDataSet[index].SelectionStart -= nrOfRemovedPositions;
                imageDataSet[index].SelectionLength = 1;
            }
        }
    }
}
