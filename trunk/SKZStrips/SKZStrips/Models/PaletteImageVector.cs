using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svg;
using System.Drawing;

namespace SKZStrips.Models
{
    public class PaletteImageVector : PaletteImage
    {
        private SvgDocument m_svgDocument;

        public override void LoadImage()
        {
            m_svgDocument = SvgDocument.Open(FullImagePath);
        }

        public override void RenderToBitmap(Bitmap target)
        {
            SvgDocument doc = (SvgDocument)m_svgDocument.Clone();
            doc.Draw(target);
        }

        public override void RenderToBitmap(Bitmap target, int x, int y, int width, int height)
        {
            SvgDocument rendingDoc = (SvgDocument)m_svgDocument.Clone();
            try
            {
                Svg.ISvgRenderer renderer = Svg.SvgRenderer.FromImage(target);
                float svgWidth = (float)rendingDoc.Width.ToDeviceValue(renderer, UnitRenderingType.Horizontal, rendingDoc);
                float svgHeight = (float)rendingDoc.Height.ToDeviceValue(renderer, UnitRenderingType.Vertical, rendingDoc);

                if (x != 0 || y != 0)
                {
                    //TODO - fix this. There is no obvious maths going on here. The values do not move the picture by the correct amount.
                    // what percentage of the bitmap are we moving?
                    float bitmapPercX = (float)x / (float)target.Width;
                    float bitmapPercY = (float)y / (float)target.Height;

                    // apply these to the SVG image
                    float svgX = bitmapPercX * svgWidth ;
                    float svgY = bitmapPercY * svgHeight;

                    Svg.Transforms.SvgTranslate translate = new Svg.Transforms.SvgTranslate(svgX, svgY);
                    rendingDoc.Transforms.Add(translate);
                }

                // all drawing is done relative to the size of the image being drawn on.
                // we only need to scale if the picture is not going to fit the entire image.
                if (width != target.Width || height != target.Height)
                {
                    float scaleX = (float)width / target.Width;
                    float scaleY = (float)height / target.Height;

                    Svg.Transforms.SvgScale scale = new Svg.Transforms.SvgScale(scaleX, scaleY);
                    rendingDoc.Transforms.Add(scale);
                }
                

                rendingDoc.Draw(target);
            }
            finally
            {
                // Transforms are not cloned but are shared with the original document.
                // They MUST therefore be cleared.
                rendingDoc.Transforms.Clear();
            }
        }
    }
}
