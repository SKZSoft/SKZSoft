using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterData.Consts;

namespace SKZSoft.Twitter.TwitterData.Models
{
    public class LocalImage
    {
        public string Path { get; set; }
        public Image Thumbnail { get; set; }

        public int IndexOnStatus { get; set; }
        public object Owner { get; set; }

        // dummy delegate. Never called, but required.
        // Source: https://msdn.microsoft.com/en-us/library/system.drawing.image.getthumbnailimage(v=vs.110).aspx
        public bool ThumbnailCallback()
        {
            return false;
        }

        public LocalImage(string path)
        {
            Path = path;
            Image fullImage = LoadImage(path);
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            double heightRatio = (double)DataConsts.THUMBNAIL_HEIGHT / (double)fullImage.Height;
            int newWidth =(int)(fullImage.Width * heightRatio);

            Thumbnail = fullImage.GetThumbnailImage(newWidth, DataConsts.THUMBNAIL_HEIGHT, myCallback, IntPtr.Zero);
        }

        private Image LoadImage(string path)
        {
            Image img = Image.FromFile(path);
            return img;
        }

    }
}
