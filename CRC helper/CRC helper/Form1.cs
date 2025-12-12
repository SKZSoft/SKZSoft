namespace CRC_helper
{
    using System.IO;
    using System.Text;
    //using static System.Net.WebRequestMethods;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public partial class frmMain : Form

    {
        private enum Mode
        {
            Generate,
            Check
        }

        public frmMain()
        {
            InitializeComponent();
            EnableDisableControls();
        }

        private void optSingleCRCFile_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableControls();
        }

        private void EnableDisableControls()
        {
            txtCRCFilePath.Enabled = optSingleCRCFile.Checked;
            txtSourceFolders.Enabled = optCRCFilePerRootFolder.Checked;
            btnGenerateCRCFile.Enabled = txtSourceFolders.Text.Length > 0;
            btnVerifyCRCFile.Enabled = (txtSourceFolders.Text.Length > 0 && txtCRCFilePath.Text.Length > 0) || optCRCFilePerRootFolder.Checked;
        }

        private void txtSourceFolders_TextChanged(object sender, EventArgs e)
        {
            EnableDisableControls();
        }

        private void txtCRCFilePath_TextChanged(object sender, EventArgs e)
        {
            EnableDisableControls();
        }

        private void btnGenerateCRCFile_Click(object sender, EventArgs e)
        {
            Dictionary<string, DirectoryInfo> directories = null;
            bool CRCFilePerFolder;
            string CRCFilePath;
            Mode mode;
            string errors;
            Dictionary<string, FileInfo> existingFiles = new Dictionary<string, FileInfo>();

            bool good = GetAndValidateFormData(out directories, out CRCFilePerFolder, out CRCFilePath, Mode.Generate, out errors, out existingFiles);

            // TODO need to check if existing crc files exist and ask to overwrite.
            // get existing files recursive call should be done in a new method, seperate to the other form data.

            if (!good)
            {
                MessageBox.Show(errors);
                return;
            }
        }

        private bool GetAndValidateFormData(out Dictionary<string, DirectoryInfo> directories, out bool CRCFilePerFolder, out string CFCFilePath, Mode mode, out string errortext, out Dictionary<string, FileInfo> existingFiles)
        {
            // get data from form
            CRCFilePerFolder = optCRCFilePerRootFolder.Checked;
            CFCFilePath = txtCRCFilePath.Text;
            StringBuilder errorsfound = new StringBuilder();
            bool allGood = true;


            // one file or per folder?
            if (optSingleCRCFile.Checked)
            {
                CRCFilePerFolder = false;
            }

            // get directories from textbox to dictionary
            directories = new Dictionary<string, DirectoryInfo>();
            string allDirectories = txtSourceFolders.Text;

            // add all directories to a dictionary, checking for and adding errors as found
            using (System.IO.StringReader reader = new System.IO.StringReader(allDirectories))
            {
                bool dirNotFound = false;
                string line = line = reader.ReadLine();
                while (line != null)
                {
                    if (Directory.Exists(line))
                    {
                        DirectoryInfo dir = new DirectoryInfo(line);
                        directories.Add(line, dir);
                    }
                    else
                    {
                        allGood = false;
                        if (!dirNotFound)
                        {
                            errorsfound.AppendLine("The following directories do not exist:");
                            dirNotFound = true;
                        }
                        errorsfound.AppendLine(line);
                    }
                    line = reader.ReadLine();
                }

                // if we are using a single CRC file then check it exists
                if (mode == Mode.Check && !CRCFilePerFolder)
                {
                    if (!File.Exists(CFCFilePath))
                    {

                        // new line if errors already exist
                        if (!allGood)
                        {
                            errorsfound.AppendLine(Environment.NewLine);
                        }

                        errorsfound.AppendLine("CRC file does not exist");
                        allGood = false;
                    }
                }

                // if there are multiple CRC files, make sure that they they all exist
                if(mode==Mode.Check && CRCFilePerFolder)
                {
                    foreach (KeyValuePair<string, DirectoryInfo> kvp in directories)
                    {
                        DirectoryInfo di = kvp.Value;
                        string expectedFileName = string.Format("{0}.SHA512", di.Name);
                        string expectedPath = string.Format("{0}\\{1}", di.FullName, expectedFileName);
                        if(!File.Exists(expectedPath))
                        {
                            if(!allGood)
                            { 
                                errorsfound.AppendLine(Environment.NewLine);
                            }
                            string newErrror = string.Format("The CRC check file {0} does not exist", expectedPath);
                            errorsfound.AppendLine(newErrror);
                            allGood = false;
                        }
                    }
                }

                // have to instantiate this as it's an out parameter. May as well do it here.
                existingFiles = new Dictionary<string, FileInfo>();


                if (!allGood)
                {
                    // no point scanning everything if we already had fatal issues
                    errortext = errorsfound.ToString();
                    return false;
                }
                // get list of all existing files, regardless of what we're going to do with them
                foreach (KeyValuePair<string, DirectoryInfo> kvp in directories)
                {
                    DirectoryInfo di = kvp.Value;
                    existingFiles = new Dictionary<string, FileInfo>();
                    GetExistingFiles(di, existingFiles);
                }

                errortext = "";
                return true;

            }
        }

        private void GetExistingFiles(DirectoryInfo di, Dictionary<string, FileInfo> files)
        {
            foreach (DirectoryInfo subdirectory in di.GetDirectories())
            {
                GetExistingFiles(subdirectory, files);
            }

            foreach(FileInfo fi in di.GetFiles())
            {
                files.Add(fi.FullName, fi);
            }
        }

        private void btnVerifyCRCFile_Click(object sender, EventArgs e)
        {
            Dictionary<string, DirectoryInfo> directories = null;
            bool CRCFilePerFolder;
            string CRCFilePath;
            Mode mode;
            string errors;
            Dictionary<string, FileInfo> existingFiles = new Dictionary<string, FileInfo>();

            bool good = GetAndValidateFormData(out directories, out CRCFilePerFolder, out CRCFilePath, Mode.Check, out errors, out existingFiles);


            if (!good)
            {
                MessageBox.Show(errors);
                return;
            }

        }
    }
}
