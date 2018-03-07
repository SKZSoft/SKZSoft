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
        public ulong next_cursor { get; set; }
        public string next_cursor_str { get; set; }

        /// <summary>
        /// The ID of the cursor which will fetch the previous batch of IDs
        /// </summary>
        public ulong previous_cursor { get; set; }
        public string previous_cursor_str { get; set; }

    }
}
