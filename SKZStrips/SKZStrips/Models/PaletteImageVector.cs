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
            float scaleX = (float)width / svgWidth;

            float svgHeight = (float)m_svgDocument.Height.ToDeviceValue(renderer, UnitRenderingType.Vertical, m_svgDocument);
            float scaleY = (float)height / svgHeight;

            Svg.Transforms.SvgScale scale = new Svg.Transforms.SvgScale(scaleX, scaleY);
            m_svgDocument.Transforms.Add(scale);
            m_svgDocument.Draw(target);

        }
    }
}
