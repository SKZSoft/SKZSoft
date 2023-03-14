
namespace DupeFinder
{
    using System.Drawing.Text;
    using System.Security.Cryptography;
    using System.Text;

    public partial class Form1 : Form
    {

        private enum formStatus
        {
            Ready,
            Working
        }
        private formStatus m_status= formStatus.Ready;
        private Dictionary<string, FileData> m_files = new Dictionary<string, FileData>();
        private Dictionary<long, Dictionary<string, FileData>> m_filesByLength = new Dictionary<long, Dictionary<string, FileData>>();
        private Dictionary<string, Dictionary<string, FileData>> m_filesByHash = new Dictionary<string, Dictionary<string, FileData>>();
        private long m_toHashCount;
        private long m_hashing;
        public Form1()
        {
            InitializeComponent();
            Status = formStatus.Ready;
        }

        private formStatus Status 
        { 
            get
            { return m_status; }
                
            set
            { 
                m_status = value; 
                txtPaths.Enabled = (value== formStatus.Ready);
                btnCheck.Enabled = (value== formStatus.Ready);
                lblStatus.Visible = (value== formStatus.Working);
                btnDelete.Enabled = (value==formStatus.Ready && grid.Rows.Count >0);

                this.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Status = formStatus.Working;
                string[] fileList = txtPaths.Text.Split(Environment.NewLine);
                m_toHashCount = 0;
                ProcessFiles(fileList);
                ShowResults();
            }
            finally
            {
                Status = formStatus.Ready;
            }
        }

        private void ProcessFiles(string[] paths)
        {
            m_files = new Dictionary<string, FileData>();
            m_filesByLength = new Dictionary<long, Dictionary<string, FileData>>();
            foreach (string path in paths)
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                ProcessPath(dir);
                FindDupes();
            }
        }

        private void FindDupes()
        {
            UpdateStatus("Checking for duplicates");
            foreach (KeyValuePair<long, Dictionary<string, FileData>> kvp in m_filesByLength)
            {
                if (kvp.Value.Count > 1)
                {
                    m_toHashCount += kvp.Value.Count;
                }
            }

            m_hashing = 0;
            foreach (KeyValuePair<long, Dictionary<string, FileData>> kvp in m_filesByLength)
            {
                if (kvp.Value.Count > 1)
                {
                    // get checksums for everything the same length
                    GetCheckSums(kvp.Value);
                }
            }
        }

        private void GetCheckSums(Dictionary<string, FileData> files)
        {
            m_filesByHash = new Dictionary<string, Dictionary<string, FileData>>();
            foreach (KeyValuePair<string, FileData> kvp in files)
            {
                GetCheckSum(kvp.Value);
            }
        }


        private void ShowResults()
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            grid.SuspendLayout();

            DataGridViewCheckBoxColumn col0 = new DataGridViewCheckBoxColumn();
            col0.HeaderText = "Delete";
            grid.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "Path";
            col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grid.Columns.Add(col1);

            foreach(KeyValuePair<string, Dictionary<string, FileData>> kvpHashes in m_filesByHash)
            {
                if (kvpHashes.Value.Count > 1)
                {
                    grid.Rows.Add(false, kvpHashes.Value.Count.ToString());
                    foreach (KeyValuePair<string, FileData> kvpFile in kvpHashes.Value)
                    {
                        grid.Rows.Add(false, kvpFile.Value.FullPath);
                    }
                }
            }

            grid.ResumeLayout();

        }


        private void GetCheckSum(FileData fd)
        {
            UpdateStatus("Hashing " + fd.FullPath);
            FileStream file;
            try
            {
                m_hashing++;
                lblCount.Text = String.Format("{0} of {1}", m_hashing, m_toHashCount);
                lblCount.Refresh();

                file = new FileStream(fd.FullPath, FileMode.Open);

                try
                { 
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    byte[] retVal = sha1.ComputeHash(file);

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }
                    fd.Hash = sb.ToString();


                    Dictionary<string, FileData> byHash = new Dictionary<string, FileData>();
                    if (m_filesByHash.ContainsKey(fd.Hash))
                    {
                        byHash = m_filesByHash[fd.Hash];
                    }
                    else
                    {
                        m_filesByHash.Add(fd.Hash, byHash);
                    }
                    byHash.Add(fd.FullPath, fd);


                }
                catch (Exception e)
                { System.Diagnostics.Debug.Print("cannot hash " + fd.FullPath); }
                finally
                {
                    file.Close();
                }
            }
            catch (Exception e)
            { System.Diagnostics.Debug.Print("cannot open " + fd.FullPath); }
        }

        private void ProcessPath(DirectoryInfo dir)
        {
            foreach(FileInfo fi in dir.GetFiles())
            {
                UpdateStatus("Scanning " + fi.FullName);
                fi.IsReadOnly = false;
                FileData fd = new FileData(fi.FullName, fi.Name, fi.Length);
                m_files.Add(fd.FullPath, fd);


                Dictionary<string, FileData> byLengthDic = new Dictionary<string, FileData>();
                if (m_filesByLength.ContainsKey(fd.Size))
                {
                    byLengthDic = m_filesByLength[fd.Size];
                }
                else
                {
                    m_filesByLength.Add(fd.Size, byLengthDic);
                }
                byLengthDic.Add(fd.FullPath, fd);

            }

            foreach (DirectoryInfo subdir in dir.GetDirectories())
            {
                ProcessPath(subdir);
            }
        }

        private void UpdateStatus(string status)
        {
            lblStatus.Text = status;
            lblStatus.Refresh();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Status = formStatus.Working;
            m_filesByHash = new Dictionary<string, Dictionary<string, FileData>>();
            AddTest("1", "a:\\test.jpg", 45346545);
            AddTest("2", "a:\\dupe1.jpg", 4235453);
            AddTest("2", "b:\\dupe2.jpg", 987569);

            ShowResults();
            Status = formStatus.Ready;
        }


        private void AddTest(string hash, string path, long size)
        {
            FileData fd = new FileData(path, path, size);
            fd.Hash = hash;
            Dictionary<string, FileData> byHash = new Dictionary<string, FileData>();
            if (m_filesByHash.ContainsKey(fd.Hash))
            {
                byHash = m_filesByHash[fd.Hash];
            }
            else
            {
                m_filesByHash.Add(fd.Hash, byHash);
            }
            byHash.Add(fd.FullPath, fd);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            long sizeDeleted = 0;
            foreach(DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;

                bool isChecked = (bool)row.Cells[0].Value;
                if (isChecked)
                {
                    string path = (string)row.Cells[1].Value;
                    FileInfo fi = new FileInfo(path);
                    sizeDeleted += fi.Length;
                    fi.Delete();
                }

            }
            long SizeInMB = (long)sizeDeleted / 1000000;
            string message = string.Format("Freed up {0} MB", SizeInMB);
            MessageBox.Show(message);
            grid.Rows.Clear();
        }
    }
}