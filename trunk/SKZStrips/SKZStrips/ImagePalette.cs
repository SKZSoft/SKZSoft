using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace SKZStrips
{
    public partial class ImagePalette : Form
    {
        public ImagePalette()
        {
            InitializeComponent();
        }

        public void Initialise(XmlNode config, string basePath)
        {
            // TODO change this. GUI should not read its own config.
            // This needs to be passed in by the controller.

            imagePicker.Initialise(config, basePath);
        }
    }
}
