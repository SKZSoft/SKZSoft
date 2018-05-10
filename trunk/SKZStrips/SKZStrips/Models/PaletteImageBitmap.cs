using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SKZStrips.Models
{
    public class PaletteImageBitmap: PaletteImage
    {
        public Bitmap Bitmap{ get; set; }

        public override void LoadImage()
        {
            Bitmap = new Bitmap(FullImagePath);
        }

        public override void RenderToBitmap(Bitmap target)
        {
            Graphics graph = Graphics.FromImage(Bitmap);
            graph.DrawImage(target, 0, 0);
        }

        public override void RenderToBitmap(Bitmap target, int x, int y, int width, int height)
        {
            Graphics graph = Graphics.FromImage(target);
            graph.DrawImage(Bitmap, x, y, width, height);
        }
    }
}
