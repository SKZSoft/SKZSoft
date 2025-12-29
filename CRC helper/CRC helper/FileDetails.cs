using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRC_helper
{
    internal class FileDetails
    {
        //TODO: change main code to use this class instead of vanilla string string dictionaries
        // then also add a dictionary for all files (or pass one back from the results form)

        public enum FileStatus
        {
            OK,
            Deleted,
            Moved,
            Changed,
            New
        }
        public FileStatus Status { get; set; }
        public string Path { get; set; }
        public string Hash { get; set; }

        public bool ChangeAccepted { get; set; }

        public FileDetails(FileStatus status, string path, string hash)
        {
            this.Status = status;
            Path = path;
            Hash = hash;
            ChangeAccepted = false;
        }

        internal string UniqueKey()
        {
            string key = string.Format("{0}-{1}", Hash, Path);
            return key;
        }
    }
}
