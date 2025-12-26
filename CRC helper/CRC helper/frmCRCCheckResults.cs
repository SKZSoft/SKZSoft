using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRC_helper
{
    public partial class frmCRCCheckResults : Form
    {
        private Dictionary<string, string> m_correctFiles;
        private Dictionary<string, string> m_changedFiles;
        private Dictionary<string, string> m_movedFiles;
        private Dictionary<string, string> m_newFilesByPath;
        private Dictionary<string, string> m_missingFiles;
        public frmCRCCheckResults(Dictionary<string, string> correctFiles,
            Dictionary<string, string> changedFiles,
            Dictionary<string, string> movedFiles,
            Dictionary<string, string> newFilesByPath,
            Dictionary<string, string> missingFiles)
        {
            InitializeComponent();

            m_correctFiles = correctFiles;
            m_changedFiles = changedFiles;
            m_movedFiles = movedFiles;
            m_newFilesByPath = newFilesByPath;
            m_missingFiles = missingFiles;

            lblChanged.Text = m_changedFiles.Count.ToString();
            lblOK.Text = m_correctFiles.Count.ToString();
            lblDeleted.Text = m_missingFiles.Count.ToString();
            lblMoved.Text = m_movedFiles.Count.ToString();
            lblNew.Text = m_newFilesByPath.Count.ToString();
        }


    }
}
