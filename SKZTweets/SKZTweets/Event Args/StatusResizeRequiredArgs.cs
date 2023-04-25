using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.SKZTweets
{

    public class StatusResizeRequiredArgs : EventArgs
    {
        public int NewHeight { get; set; }

        public StatusResizeRequiredArgs(int newHeight)
        {
            NewHeight = newHeight;
        }
    }
}
