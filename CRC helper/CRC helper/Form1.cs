namespace CRC_helper
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection.Emit;
    using System.Security.Cryptography;
    using System.Text;
    //using static System.Net.WebRequestMethods;
    //using static System.Net.WebRequestMethods;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public partial class frmMain : Form

    {
        private enum Mode
        {
            Generate,
            Check
        }

        private Dictionary<string, string> m_calculatedCRCsByPath;
        private Dictionary<string, string> m_calculatedCRCsByHash;
        private Dictionary<string, string> m_correctFiles;
        private Dictionary<string, string> m_changedFiles;
        private Dictionary<string, string> m_movedFiles;
        private Dictionary<string, string> m_newFilesByPath;
        private Dictionary<string, string> m_missingFiles;
        private bool m_changesDetected;

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

        /// <summary>
        /// Generate a new CRC file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateCRCFile_Click(object sender, EventArgs e)
        {
            Dictionary<string, DirectoryInfo> directories = null;
            bool CRCFilePerFolder;
            string CRCFilePath;
            Mode mode;
            string errors;
            Dictionary<string, FileInfo> existingFiles = new Dictionary<string, FileInfo>();

            lblProcessingFile.Text = "";
            InitialiseClassData();

            bool good = GetAndValidateFormData(out directories, out CRCFilePerFolder, out CRCFilePath, Mode.Generate, out errors, out existingFiles);

            //Sanity check the output file
            string outputFile = txtCRCFilePath.Text;
            if (Directory.Exists(outputFile))
            {
                MessageBox.Show("Output file already exists and is a directory");
                return;
            }

            FileInfo fiOutput = new FileInfo(outputFile);
            if (fiOutput.Exists)
            {
                if (fiOutput.IsReadOnly)
                {
                    MessageBox.Show("Output file already exists and is read only");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Do you want to overwrite the existing CRC file?", "Overwrite CRC file?", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }

                }
            }

            // TODO need to check if existing crc files exist and ask to overwrite.
            // get existing CRC files call should be done in a new method, seperate to the other form data.

            if (!good)
            {
                MessageBox.Show(errors);
                return;
            }

            Dictionary<string, string> CRCsByPath;

            GetCRCSForFiles(existingFiles);

            if (m_calculatedCRCsByPath.Count != existingFiles.Count)
            {
                MessageBox.Show("Could not generate all CRCs");
                return;
            }

            // if we got to this point, we are overwriting and the GUI has already checked.
            if(File.Exists(CRCFilePath))
            {
                // but let's take a backup anyway
                string backupFilename = string.Format("{0}.bak", CRCFilePath);
                if(File.Exists(backupFilename))
                {
                    File.Delete(backupFilename);
                    File.Copy(CRCFilePath, backupFilename);
                }
                File.Delete(CRCFilePath);
            }

            // write the CRCs to the new CRC file
            using (StreamWriter writer = new StreamWriter(CRCFilePath, false))
            {
                string CRCFileDirectory = Path.GetDirectoryName(CRCFilePath);
                DirectoryInfo di = new DirectoryInfo(CRCFileDirectory);

                string pathToReplace = string.Format("{0}\\", di.FullName);
                foreach (KeyValuePair<string,string>  kvp in m_calculatedCRCsByPath)
                {
                    // get the path *relative* to the CRC file
                    string fullPath = kvp.Key;

                    // this should include a drive letter so we should be safe to simply replace the CRC directory path for each file
                    string relativePath = fullPath.Replace(pathToReplace, "", StringComparison.InvariantCultureIgnoreCase);

                    // build a line with CRC then space then asterix (for RapidCRC compatability) then relative path
                    string CRCLine = string.Format("{0} *{1}", kvp.Value, relativePath);

                    //write the line
                    writer.WriteLine(CRCLine);
                }
            }
            MessageBox.Show("CRC File generated");
            lblProcessingFile.Text = "";
        }

        private void GetCRCSForFiles(Dictionary<string, FileInfo> existingFiles)
        {
            foreach (FileInfo fi in existingFiles.Values)
            {
                lblProcessingFile.Text = fi.FullName;
                lblProcessingFile.Refresh();

                // taken and adpted from
                // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256?view=net-9.0
                using (SHA512 hash = SHA512.Create())
                {
                    // Compute and print the hash values for each file in directory.
                    using (FileStream fileStream = fi.Open(FileMode.Open))
                    {
                        try
                        {
                            // Create a fileStream for the file.
                            // Be sure it's positioned to the beginning of the stream.
                            fileStream.Position = 0;

                            // Compute the hash of the fileStream.
                            byte[] hashValue = hash.ComputeHash(fileStream);

                            //string hashString = System.Text.Encoding.UTF8.GetString(hashValue);
                            string hashString = BitConverter.ToString(hashValue);

                            // HACK: there really ought to be an output option which doesn't include "-" as a delimiter inthe above method
                            // but there isn't and I've already spent an hour on what should be (these days) be a simple task to generate
                            // a CRC string from a binary file via an in-built library
                            hashString = hashString.Replace("-", "");

                            // make the hash lower case
                            hashString = hashString.ToLower();

                            // add it
                            m_calculatedCRCsByPath.Add(fi.FullName, hashString);
                            m_calculatedCRCsByHash.Add(hashString, fi.FullName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(string.Format("Error %0", ex.Message));
                            return;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Form data collection and validation
        /// Returns error string to display if needed
        /// </summary>
        /// <param name="directories"></param>
        /// <param name="CRCFilePerFolder"></param>
        /// <param name="CFCFilePath"></param>
        /// <param name="mode"></param>
        /// <param name="errortext"></param>
        /// <param name="existingFiles"></param>
        /// <returns></returns>
        private bool GetAndValidateFormData(out Dictionary<string, DirectoryInfo> directories, out bool CRCFilePerFolder, out string CRCFilePath, Mode mode, out string errortext, out Dictionary<string, FileInfo> existingFiles)
        {
            // get data from form
            CRCFilePerFolder = optCRCFilePerRootFolder.Checked;
            CRCFilePath = txtCRCFilePath.Text;
            StringBuilder errorsfound = new StringBuilder();
            bool allGood = true;


            // one file or per folder?
            if (optSingleCRCFile.Checked)
            {
                CRCFilePerFolder = false;
            }
            else
            {
                allGood = false;
                errorsfound.AppendLine("CRC file per form not yet implemented.");
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

                // TODO: Not yet implemented to have multiple input folders
                // This is because files should be compatible with RapidCRC
                // and that assumes a single input folder which is the base
                // path for all other files.
                // If different folders are allowed, it would have to be the full path
                // and that includes drive letters which may change.
                // The files may even be on different drives so relative paths can't be used.
                if(directories.Count > 1)
                {
                    allGood = false;
                    errorsfound.AppendLine("More than one folder not currently supported");
                }

                // if we are checking and using a single CRC file then check it exists
                if (mode == Mode.Check && !CRCFilePerFolder)
                {
                    if (!File.Exists(CRCFilePath))
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

                /* TODO
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
                */

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
                    GetExistingFiles(di, existingFiles);
                }

                // eliminate the CRC file
                if(existingFiles.ContainsKey(CRCFilePath))
                {
                    existingFiles.Remove(CRCFilePath);
                }

                errortext = "";
                return true;

            }
        }

        /// <summary>
        /// Get all the files in the listed folders and sub-folders into the dictionaries to pass back to other methods. 
        /// Recursive.
        /// </summary>
        /// <param name="di"></param>
        /// <param name="files"></param>
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


        private void InitialiseClassData()
        {
            m_changedFiles = new Dictionary<string, string>();
            m_correctFiles = new Dictionary<string, string>();
            m_missingFiles = new Dictionary<string, string>();
            m_movedFiles = new Dictionary<string, string>();
            m_newFilesByPath = new Dictionary<string, string>();
            m_calculatedCRCsByHash = new Dictionary<string, string>();
            m_calculatedCRCsByPath = new Dictionary<string, string>();
        }

        /// <summary>
        /// Verify CRC file(s)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerifyCRCFile_Click(object sender, EventArgs e)
        {
            Dictionary<string, DirectoryInfo> directories;
            bool CRCFilePerFolder;
            string CRCFilePath;
            Mode mode;
            string errors;

            InitialiseClassData();

            // get data from form and initialise form
            Dictionary<string, FileInfo> existingFiles = new Dictionary<string, FileInfo>();
            bool good = GetAndValidateFormData(out directories, out CRCFilePerFolder, out CRCFilePath, Mode.Check, out errors, out existingFiles);
            lblProcessingFile.Text = "";

            // abort if any errors were found
            if (!good)
            {
                MessageBox.Show(errors);
                return;
            }

            GetCRCSForFiles(existingFiles);


            if (m_calculatedCRCsByPath.Count != existingFiles.Count)
            {
                MessageBox.Show("Could not generate all CRCs");
                return;
            }

            // now scan the existing CRC file into an identical dictionary, for comparison
            Dictionary<string, string> oldCRCsByPath = new Dictionary<string, string>();
            Dictionary<string, string> oldCRCsByHash = new Dictionary<string, string>();

            FileInfo fi = new FileInfo(CRCFilePath);
            DirectoryInfo di = new DirectoryInfo(fi.DirectoryName);
            string relativePathToAdd = di.FullName;

            ReadOldCRCs(CRCFilePath, oldCRCsByPath, oldCRCsByHash, relativePathToAdd);

            // results are put into class-level dictionaries
            // because we are just going to display the results and then the user may take actions
            // which will result in changes to the dictionaries which have to be persisted
            CompareCRCs(oldCRCsByPath, oldCRCsByHash, CRCFilePath);

            DisplayResults();

        }

        private void DisplayResults()
        {
            if(!m_changesDetected)
            {
                // Everything checks out
                MessageBox.Show("All CRCs verified.");
                lblProcessingFile.Text = "CRCs Verified";
            }
        }

        private static void ReadOldCRCs(string CRCFilePath, Dictionary<string, string> oldCRCsByPath, Dictionary<string, string> oldCRCsByHash, string relativePathToAdd)
        {
            using (StreamReader streamReader = new StreamReader(CRCFilePath))
            {
                string? line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);

                    // remove asterix and make it a full path, not relative
                    string path = parts[1].Replace("*", "");
                    path = string.Format("{0}\\{1}", relativePathToAdd, path);


                    oldCRCsByPath.Add(path, parts[0]);
                    oldCRCsByHash.Add(parts[0], path);

                    line = streamReader.ReadLine();
                }

            }
        }

        /// <summary>
        /// Create dictionaries for:
        ///     exact matches
        ///     missing
        ///     changed
        ///     same CRC, different name
        ///     new files
        /// </summary>
        /// <param name="oldCRCs"></param>
        /// <param name="newCRCs"></param>
        private void CompareCRCs(Dictionary<string, string> oldCRCsByPath, Dictionary<string, string> oldCRCsByHash, string CRCFilePath)
        {
            // check all existing files
            m_changesDetected = false;
            foreach (KeyValuePair<string, string> kvp in oldCRCsByPath)
            {
                string path = kvp.Key;
                string hash = kvp.Value;

                // work out if file is OK, changed, moved, or missing
                if(m_calculatedCRCsByPath.ContainsKey(path))
                {
                    string oldHash = oldCRCsByPath[path];
                    if (oldHash == hash)
                    {
                        // this is an match.
                        m_correctFiles.Add(kvp.Key, kvp.Value);
                    }
                    else
                    {
                        // the file exists but with a different hash
                        m_changedFiles.Add(path, hash);
                        m_changesDetected = true;
                    }
                }
                else
                {
                    // this file is not in the new set of CRCs
                    // but does its hash exist? Has it moved?
                    if (m_calculatedCRCsByHash.ContainsKey(hash))
                    {
                        m_movedFiles.Add(path, hash);
                        m_changesDetected = true;
                    }
                    else
                    {
                        m_missingFiles.Add(path, hash);
                        m_changesDetected = true;
                    }
                }
            }

            // we have now processed all the OLD files. We need to check for new files
            foreach(KeyValuePair<string, string> kvp in m_calculatedCRCsByPath)
            {
                string newPath = kvp.Key;
                string newHash = kvp.Value;

                // first check if the path exists in any of the result dictionaries
                bool fileExists = false;    // pessimism
                if(m_correctFiles.ContainsKey(newPath))
                {
                    fileExists = true;
                }

                if (m_changedFiles.ContainsKey(newPath))
                {
                    fileExists = true;
                }

                // get the old path
                string oldPath = m_calculatedCRCsByHash[newHash];

                if(m_movedFiles.ContainsKey(oldPath))
                {
                    fileExists = true;
                }

                if (!fileExists)
                {
                    // it's notcorrect, changed, or moved.
                    // it can't be missing because these are the new hashes so we don't have that data.
                    // it must therefore be new
                    m_newFilesByPath.Add(newPath, newHash);
                    m_changesDetected = true;
                }
            }

        }

    }
}
