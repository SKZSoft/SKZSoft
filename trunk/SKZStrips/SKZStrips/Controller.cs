using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace SKZStrips
{
    public class Controller
    {
        // TODO softcode this and put it somewhere sensible
        const string configPath = @"d:\GFX\";
        const string configFilename = @"SKZStrips config.xml";

        private List<ImagePalette> m_imagePalettes;

        public List<ImagePalette> ImagePalettes {  get { return m_imagePalettes; } }

        public frmMain Initialise()
        {
            XmlDocument xmlConfig = new XmlDocument();
            string fullConfigPath = Path.Combine(configPath, configFilename);
            xmlConfig.Load(fullConfigPath);

            XmlNode palettes = xmlConfig.SelectSingleNode("/config/palettes");
            m_imagePalettes = new List<ImagePalette>();
            
            foreach(XmlNode paletteConfig in palettes.ChildNodes)
            {
                ImagePalette paletteForm = new ImagePalette();
                paletteForm.Initialise(paletteConfig, configPath);
                m_imagePalettes.Add(paletteForm);
            }


            frmMain mainForm = new frmMain();
            mainForm.Initialise(this);
            return mainForm;
        }
        
    }
}
