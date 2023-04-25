using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterModels
{
    public class Photo_Sizes
    {
        public Large large { get; set; }
        public Medium medium { get; set; }
        public Small small { get; set; }
        public Thumb thumb { get; set; }
    }

    public class Large
    {
        public int h { get; set; }
        public string resize { get; set; }
        public int w { get; set; }
    }

    public class Medium
    {
        public int h { get; set; }
        public string resize { get; set; }
        public int w { get; set; }
    }

    public class Small
    {
        public int h { get; set; }
        public string resize { get; set; }
        public int w { get; set; }
    }

    public class Thumb
    {
        public int h { get; set; }
        public string resize { get; set; }
        public int w { get; set; }
    }
}
