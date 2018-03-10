using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterModels
{

    public class FollowerIds
    {
        /// <summary>
        /// Array of IDs (can be empty)
        /// </summary>
        public ulong[] ids { get; set; }

        /// <summary>
        /// The ID of the cursor which will fetch the next batch of IDs
        /// </summary>
        public string next_cursor_str { get; set; }

        // Twitter returns SIGNED numbers which are way too big for a long and cannot be stored in a ulong.
        // Hence we are forced to use strings only.
        // public ulong next_cursor { get; set; }
        // public ulong previous_cursor { get; set; }


        /// <summary>
        /// The ID of the cursor which will fetch the previous batch of IDs
        /// </summary>
        public string previous_cursor_str { get; set; }
        

    }
}
