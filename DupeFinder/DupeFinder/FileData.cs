using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DupeFinder
{
    internal class FileData
    {
        public string FullPath { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }

        public string Hash { get; set; }

        public FileData(string fullPath, string name, long size)
        {
            FullPath = fullPath;
            Name = name;
            Size = size;
            Hash = string.Empty;
        }
    }
}
