using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Svg;



namespace SKZStrips.Usercontrols.Palettes
{
    public partial class ImagePicker : UserControl
    {
        XmlNode m_config;
        SortedList<int, Models.PaletteImage> m_images;

        public ImagePicker()
        {
            InitializeComponent();
        }

        public void Initialise(XmlNode config, string basePath)
        {
            m_config = config;
            LoadImages(basePath);
            ShowImages();
        }


        private void ShowImages()
        {
            int X = 0;
            foreach(KeyValuePair<int, Models.PaletteImage> kvp in m_images)
            {
                Models.PaletteImage image = kvp.Value;
                PictureBox picBox = new PictureBox();
                Bitmap bitmap = new Bitmap(100, 100);
                image.RenderToBitmap(bitmap, 0, 0, 100, 100);
                picBox.Image = bitmap;
                picBox.Tag = kvp.Key;
                this.Controls.Add(picBox);
                picBox.Width = 100;
                picBox.Height = 100;
                picBox.Visible = true;
                picBox.Left = X;
                picBox.BorderStyle = BorderStyle.Fixed3D;
                picBox.MouseDown += PicBox_MouseDown;

                X += 120;
            }
        }

        private void PicBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox picBox = (PictureBox)sender;
            int key = (int)picBox.Tag;
            Models.PaletteImage image = m_images[key];
            DoDragDrop(image, DragDropEffects.Copy);
        }

        private void LoadImages(string basePath)
        {
            if(m_config==null)
            {
                throw new Exception("Code must call the constructor which takes arguments. Default constructor is only for IDE.");
            }
            m_images = new SortedList<int, Models.PaletteImage>();
            int index = 0;
            foreach(XmlNode child in m_config)
            {
                //TODO exception handling
                string type = GetXMLAttributeAsString(child, Consts.PaletteImage.xmlPaletteImage_Type);

                Models.PaletteImage image;

                switch(type)
                {
                    case "bitmap":
                        image = new Models.PaletteImageBitmap();
                        image.ImageType = Models.PaletteImageType.Bitmap;
                        break;

                    case "vector":
                        image = new Models.PaletteImageVector();
                        image.ImageType = Models.PaletteImageType.Vector;
                        break;

                    default:
                        throw new Exception("Invalid image type type");
                }

                image.BasePath = basePath;
                image.Filename = GetXMLAttributeAsString(child, Consts.PaletteImage.xmlPaletteImage_Filename);
                image.DefaultHeight = GetXMLAttributeAsInt(child, Consts.PaletteImage.xmlPaletteImage_DefaultHeight);
                image.DefaultWidth = GetXMLAttributeAsInt(child, Consts.PaletteImage.xmlPaletteImage_DefaultWidth);

                image.LoadImage();

                m_images.Add(index, image);
                index++;
            }
        }


        private int GetXMLAttributeAsInt(XmlNode node, string name)
        {
            XmlNode attribute = node.Attributes.GetNamedItem(name);
            if(attribute==null)
            {
                throw new Exception(string.Format("attribute {0} does not exist", name));
            }

            string tmp = attribute.Value;

            int number;
            if(int.TryParse(tmp, out number))
            {
                return number;
            }

            // invalid number
            throw new XmlException(string.Format("Attribute {0} is not numeric", name));
        }

        private string GetXMLAttributeAsString(XmlNode node, string name)
        {
            XmlNode attribute = node.Attributes.GetNamedItem(name);
            if (attribute == null)
            {
                throw new Exception(string.Format("attribute {0} does not exist", name));
            }

            return attribute.Value;
        }


    }
}
