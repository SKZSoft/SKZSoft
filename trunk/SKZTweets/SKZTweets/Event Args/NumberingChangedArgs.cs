using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZTweets.TwitterData.Models;

namespace SKZTweets
{
    public class NumberingChangedArgs : EventArgs
    {
        public ThreadNumberSettings NumberingSettings { get; set; }

        public NumberingChangedArgs(ThreadNumberSettings settings)
        {
            NumberingSettings = settings;
        }
    }
}
