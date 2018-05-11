using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace SKZStrips
{

    // IMPORTANT INFO
    // The source version of the SVG package is found here:
    // https://github.com/vvvv/SVG/tree/ecda67661da94793ffbc3b3262c259c5bafb5207
    // It is not the LATEST version - that has a different default scaling behaviour.

    /// <summary>
    /// Main controller
    /// </summary>
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
