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
        private bool m_saveChanges = false;
        private Dictionary<string, string> m_correctFiles;
        private Dictionary<string, string> m_changedFiles;
        private Dictionary<string, string> m_movedFiles;
        private Dictionary<string, string> m_newFilesByPath;
        private Dictionary<string, string> m_missingFiles;
        private Dictionary<string, string> m_couldNotcalculate;
        public frmCRCCheckResults(Dictionary<string, string> correctFiles,
            Dictionary<string, string> changedFiles,
            Dictionary<string, string> movedFiles,
            Dictionary<string, string> newFilesByPath,
            Dictionary<string, string> missingFiles,
            Dictionary<string, string> couldNotcalculate)
        {
            InitializeComponent();

            m_correctFiles = correctFiles;
            m_changedFiles = changedFiles;
            m_movedFiles = movedFiles;
            m_newFilesByPath = newFilesByPath;
            m_missingFiles = missingFiles;
            m_couldNotcalculate = couldNotcalculate;

            lblChanged.Text = m_changedFiles.Count.ToString();
            lblOK.Text = m_correctFiles.Count.ToString();
            lblDeleted.Text = m_missingFiles.Count.ToString();
            lblMoved.Text = m_movedFiles.Count.ToString();
            lblNew.Text = m_newFilesByPath.Count.ToString();
            lblCouldNotCalculate.Text = m_couldNotcalculate.Count.ToString();
        }

        private void frmCRCCheckResults_Load(object sender, EventArgs e)
        {

        }

        private void btnViewOK_Click(object sender, EventArgs e)
        {
            ShowFiles("OK", m_correctFiles);
        }

        private void btnViewMoved_Click(object sender, EventArgs e)
        {
            ShowFiles("Moved", m_movedFiles);
        }

        private void btnViewDeleted_Click(object sender, EventArgs e)
        {
            ShowFiles("Deleted", m_missingFiles);
        }

        private void ShowFiles(string title, Dictionary<string, string> files)
        {
            Dictionary<string, string> excludePaths = new Dictionary<string, string>();
            string[] t = txtExcludePaths.Text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            grdResults.Rows.Clear();
            grdResults.Columns.Clear();
            grdResults.Columns.Add("Col0", "title");
            foreach (KeyValuePair<string, string> kvp in files)
            {
                bool display = true;
                string path = kvp.Key;
                foreach(string excluded in t)
                {
                    if(path.Contains(excluded)) 
                    { display = false; break; }
                }

                if (display)
                {
                    grdResults.Rows.Add(kvp.Key);
                }
            }
            grdResults.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        }

        private void btnViewChanged_Click(object sender, EventArgs e)
        {
            ShowFiles("Changed", m_changedFiles);
        }

        private void btnViewNew_Click(object sender, EventArgs e)
        {
            ShowFiles("New", m_newFilesByPath);
        }

        public bool ShowResults()
        {
            this.ShowDialog();
            return m_saveChanges;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_saveChanges = false;
            this.Hide();
        }

        private void btnSaveNewFile_Click(object sender, EventArgs e)
        {
            m_saveChanges = true;
            this.Hide();
        }

        private void btnViewFailed_Click(object sender, EventArgs e)
        {
            ShowFiles("Failed to get CRC", m_couldNotcalculate);
        }
    }
}
