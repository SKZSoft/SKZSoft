using System.IO;
using System.Windows.Forms.Design;

namespace OpenRandom
{
    public partial class Form1 : Form
    {
        private Dictionary<int, string> files = new Dictionary<int, string>();

        public Form1()
        {
            InitializeComponent();
        }


        private void GetFiles(int minPicSize)
        {
            files = new Dictionary<int, string>();
            string allWildcards = "";
            if (chkVideos.Checked)
            {
                allWildcards = txtWildcardsVideo.Text;
            }

            if (chkPics.Checked)
            {
                if (allWildcards.Length > 0)
                {
                    allWildcards = allWildcards + ";";
                }
                allWildcards += txtWildCardsPics.Text; ;
            }

            string root = txtPath.Text;
            string[] wildcards = allWildcards.Split(";");

            string[] excludes = txtExclude.Text.ToLower().Split(";");

            DirectoryInfo rootDir = new DirectoryInfo(root);
            if(!rootDir.Exists)
            {
                MessageBox.Show("Directory not found.");
                return;
            }

            foreach (DirectoryInfo di in rootDir.GetDirectories())
            {
                try
                {
                    AddFiles(di, files, wildcards, minPicSize, excludes);
                }
                catch
                {
                }
            }
            lblSearching.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int minSize = int.Parse(txtMinPicSize.Text);
            minSize *= 1024;

            try
            {
                lstOpened.SuspendLayout();
                GetFiles(minSize);
            }
            finally { lstOpened.ResumeLayout(); }


            if (files.Count == 0)
            {
                MessageBox.Show("No files found");
                return;
            }

            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int index = rnd.Next(files.Count - 1);
            string filename = files[index];
            lstOpened.Items.Add(filename);
            lstOpened.SelectedIndex = lstOpened.Items.Count - 1;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = filename;
            p.StartInfo.UseShellExecute = true;
            p.Start();
        }


        private void AddFiles(DirectoryInfo di, Dictionary<int, string> files, string[] wildcards, int minPicSize, string[] excludes)
        {
            lblSearching.Text = di.FullName;
            lblSearching.Refresh();
            foreach (string wildcard in wildcards)
            {
                FileInfo[] filesInDir = di.GetFiles(wildcard);
                foreach (FileInfo fi in filesInDir)
                {
                    if (fi.Length >= minPicSize)
                    {
                        bool include = true;
                        string fname = fi.FullName.ToLower();

                        if (excludes.Length > 0)
                        {
                            foreach (string exclude in excludes)
                            {
                                if (fname.IndexOf(exclude) > 0)
                                {
                                    include = false;
                                    break;
                                }
                            }
                        }

                        if (include)
                        {
                            lblSearching.Text = fi.FullName;
                            lblSearching.Refresh();
                            files.Add(files.Count, fi.FullName);
                        }
                    }
                }
            }

            foreach (DirectoryInfo subDir in di.GetDirectories())
            {
                AddFiles(subDir, files, wildcards, minPicSize, excludes);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblSearching.Text = "";
            System.Reflection.Assembly? entryAssembly = System.Reflection.Assembly.GetEntryAssembly();

            string path = "";
            // sensible default if nothing came back
            if (entryAssembly is null)
            {
                path = "c:\\";
            }
            else
            {
                path = entryAssembly.Location;
            }

            string? drive = Path.GetPathRoot(path);
            txtPath.Text = drive;
            //txtPath.Text = "K:\\";
        }

        private void chkPics_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lstOpened_DoubleClick(object sender, EventArgs e)
        {
            OpenSelected();
        }

        private void lstOpened_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                OpenSelected();
            }
        }

        private void OpenSelected()
        {
            string? filename = lstOpened.SelectedItem as string;
            if (filename is null)
                return;

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = filename;
            p.StartInfo.UseShellExecute = true;
            p.Start();

        }

        private void lstOpened_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string? filename = lstOpened.SelectedItem as string;
            if (filename is null)
                return;

            string? folder = Path.GetDirectoryName(filename);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = folder;
            p.StartInfo.UseShellExecute = true;
            p.Start();

        }
    }
}