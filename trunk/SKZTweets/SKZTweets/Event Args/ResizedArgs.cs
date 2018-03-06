using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets
{ 
    public class ResizedArgs : EventArgs
    {
        public int NewWidth { get; set; }
        public int NewHeight { get; set; }

        public ResizedArgs(int newWidth, int newHeight)
        {
            NewWidth = newWidth;
            NewHeight = newHeight;
        }
    }
}
