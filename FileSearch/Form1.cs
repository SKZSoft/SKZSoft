namespace FileSearch
{
    using System.Diagnostics;
    using System.IO;
    public partial class Form1 : Form
    {
        Dictionary<string, FileInfo> m_files = new Dictionary<string, FileInfo>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            grdResults.Rows.Clear();
            grdResults.Columns.Clear();
            grdResults.Columns.Add("File", "File");
            grdResults.Refresh();

            DirectoryInfo di = new DirectoryInfo(txtPath.Text);
            m_files = new Dictionary<string, FileInfo>();
            Scan(di);

            string[] includeOnly = txtIncludeOnly.Text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string[] exclude = txtExclude.Text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // check if it's in the "include only" list
            foreach (KeyValuePair<string, FileInfo> kvp in m_files)
            {
                bool includesPassed = true;
                bool include = true;    // assume it's going in
                if (includeOnly.Length > 0 || exclude.Length > 0)
                {
                    //include = true;    

                    if (includeOnly.Length > 0)
                    {
                        FileInfo fi = kvp.Value;
                        if (includeOnly.Length > 0)
                        {
                            foreach (string toInclude in includeOnly)
                            {
                                if (!fi.FullName.Contains(toInclude))
                                {
                                    includesPassed = false;
                                    include = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            include = true;
                        }

                        // if it's included, is it subsequently excluded?
                        if (includesPassed)
                        {
                            foreach (string toExclude in exclude)
                            {
                                if (fi.FullName.Contains(toExclude))
                                {
                                    include = false;
                                }
                            }
                        }

                        // add to results
                        if (include)
                        {
                            grdResults.Rows.Add(fi.FullName);
                        }

                    }
                }
            }
            grdResults.AutoResizeColumns();

        }

        private void Scan(DirectoryInfo di)
        {
            foreach (FileInfo fi in di.GetFiles())
            {
                m_files.Add(fi.FullName, fi);
            }

            foreach (DirectoryInfo di2 in di.GetDirectories())
            {
                Scan(di2);
            }
        }

        private void grdResults_DoubleClick(object sender, EventArgs e)
        {
        }

        private void grdResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            //DataGridViewAutoSizeRowMode row = 
            string file = grdResults.Rows[row].Cells[0].Value.ToString();
            ProcessStartInfo pi = new ProcessStartInfo(file) { UseShellExecute = true };
            Process.Start(pi);
        }
    }
}
