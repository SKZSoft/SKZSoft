using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRC_helper
{
    /// <summary>
    /// File paths can never be duplicates. but CRC hashes can if there is more than one copy of a file
    /// So this class wraps the functionality of seeing which files share which CRC
    /// Used even for uniaue CRCs, for simple code
    /// </summary>
    internal class FilesForHash
    {
        internal FilesForHash(string newCRC)
        {
            CRC = newCRC;
        }
        public string CRC { get; set; }
        private Dictionary<string, string> m_files = new Dictionary<string, string>();

        internal bool PathExists(string path)
        {
            return m_files.ContainsKey(path);
        }

        internal void AddPath(string path)
        {
            // using path as both key and value becuse we already know the CRC
            m_files.Add(path, path);
        }

        internal Dictionary<string, string> GetPaths()
        {
            return new Dictionary<string, string>(m_files);
        }
    }
}
