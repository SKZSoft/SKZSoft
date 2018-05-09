using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace SKZStrips.Models
{

    public enum PaletteImageType
    {
        Bitmap,
        Vector
    }

    public abstract class PaletteImage
    {
        public string Filename { get; set; }
        public string BasePath { get; set; }
        public int DefaultWidth { get; set; }
        public int DefaultHeight { get; set; }
        public PaletteImageType ImageType { get; set; }
        public string FullImagePath
        {
            get
            {
                return Path.Combine(BasePath, Filename);
            }
        }

        /// <summary>
        /// Method to load the image into memory
        /// </summary>
        public abstract void LoadImage();

        /// <summary>
        /// Render image to a bitmap
        /// </summary>
        /// <param name="target"></param>
        public abstract void RenderToBitmap(Bitmap target);

        /// <summary>
        /// Render to a bitmap at a given size
        /// </summary>
        /// <param name="target"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public abstract void RenderToBitmap(Bitmap target, int x, int y, int width, int height);
    }
}
