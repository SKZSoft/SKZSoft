using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PhotoRenamer
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> m_paths;

        public Form1()
        {
            InitializeComponent();
            m_paths = new Dictionary<string, string>();
        }



        private void btnPathsClear_Click(object sender, EventArgs e)
        {
            lstPaths.Items.Clear();
            m_paths = new Dictionary<string, string>();
        }

        private void lstPaths_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
            e.Effect = DragDropEffects.None;
        }

        private void lstPaths_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = ((string[])e.Data.GetData(DataFormats.FileDrop));

            foreach (string path in paths)
            {
                if(!m_paths.ContainsKey(path))
                {
                    lstPaths.Items.Add(path);
                    m_paths.Add(path, path);
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            foreach(KeyValuePair<string, string> kvp in m_paths)
            {
                ProcessPath(kvp.Key);
            }
        }

        private void ProcessPath(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            string dateString = GetDateFromPath(path);

            ProcessPathRecurse(di, dateString);
        }


        private void ProcessPathRecurse(DirectoryInfo di, string lastDate)
        {
            string date = GetDateToUse(di, lastDate);

            foreach(DirectoryInfo subDir in di.GetDirectories())
            {
                ProcessPathRecurse(subDir, date);
            }

            foreach(FileInfo fi in di.GetFiles())
            {
                // only do JPGS
                if(fi.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                {
                    StringBuilder newName = new StringBuilder();
                    newName.Append(date);
                    newName.Append(" ");
                    newName.Append(fi.Name);
                    string fullNewPath = Path.Combine(fi.Directory.FullName, newName.ToString());
                    fi.MoveTo(fullNewPath);
                }
            }

        }

        private string GetDateToUse(DirectoryInfo di, string lastDate)
        {
            string newDate = GetDateFromPath(di.Name);
            if (String.IsNullOrEmpty(lastDate))
            {
                // If we arrived here with no idea about the date, take whatever we just decided is the date
                lastDate = newDate;
            }
            else
            {
                // If we HAD a date but now have a NEW date, use the NEW date instead
                if (!String.IsNullOrEmpty(newDate))
                {
                    lastDate = newDate;
                }
            }

            return lastDate;
        }

        private string GetDateFromPath(string path)
        {
            if(path.Length < 8)
            {
                return null;
            }

            string year = path.Substring(0, 4);
            string nextChar = path.Substring(4, 1);
            string month;
            string day;
            int tmp;
            if (int.TryParse(nextChar, out tmp))
            {
                month = path.Substring(3, 2);
                day = path.Substring(5, 2);
            }
            else
            {
                if(path.Length < 10)
                {
                    return null;
                }
                month = path.Substring(5, 2);
                day = path.Substring(8, 2);
            }

            if (!int.TryParse(year, out tmp))
                return null;
            if (!int.TryParse(month, out tmp))
                return null;
            if (!int.TryParse(day, out tmp))
                return null;


            StringBuilder sb = new StringBuilder();
            sb.Append(year);
            sb.Append("-");
            sb.Append(month);
            sb.Append("-");
            sb.Append(day);

            return sb.ToString();

        }


    }
}
