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
            m_svgDocument.Draw(target);
        }

        public override void RenderToBitmap(Bitmap target, int x, int y, int width, int height)
        {

            Svg.ISvgRenderer renderer = Svg.SvgRenderer.FromImage(target);
            float svgWidth = (float)m_svgDocument.Width.ToDeviceValue(renderer, UnitRenderingType.Horizontal, m_svgDocument);
            float svgHeight = (float)m_svgDocument.Height.ToDeviceValue(renderer, UnitRenderingType.Vertical, m_svgDocument);

            // all drawing is done relative to the size of the image being drawn on.
            // we only need to scale if the picture is not going to fit the entire image.
            if (width != target.Width || height != target.Height)
            {
                float scaleX = (float)width / target.Width;
                float scaleY = (float)height / target.Height;

                Svg.Transforms.SvgScale scale = new Svg.Transforms.SvgScale(scaleX, scaleY);
                m_svgDocument.Transforms.Add(scale);
            }

            if(x!=0 || y!=0)
            {
                //TODO - fix this. There is no obvious maths going on here. The values do not move the picture by the correct amount.
                // what percentage of the bitmap are we moving?
                float bitmapPercX = (float)x / (float)target.Width;
                float bitmapPercY = (float)y / (float)target.Height;

                // apply these to the SVG image
                float svgX = bitmapPercX * m_svgDocument.Width;
                float svgY = bitmapPercY * m_svgDocument.Height;

                Svg.Transforms.SvgTranslate translate = new Svg.Transforms.SvgTranslate(svgX, svgY);
                m_svgDocument.Transforms.Add(translate);
            }

            m_svgDocument.Draw(target);

        }
    }
}
