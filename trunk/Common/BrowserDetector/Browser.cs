using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.BrowserDetector
{

    public class Browser
    {
        public string Name { get; set; }
        public string ShellCommand { get; set; }
        public string Id { get; set; }

        public Browser(string id, string name, string shellCommand)
        {
            Id = id;
            Name = name;
            ShellCommand = shellCommand;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool IsDefault { get { return Id == Consts.DEFAULT_ID; } }

    }
}
