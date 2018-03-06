using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets
{
    /// <summary>
    /// Where to add the schedule clause: before or after the current one
    /// </summary>
    public enum AddScheduleClausePosition
    {
        Before,
        After
    };

    public class AddScheduleClauseArgs : EventArgs
    {
        public AddScheduleClausePosition Position { get; set; }

        public AddScheduleClauseArgs(AddScheduleClausePosition position)
        {
            Position = position;
        }
    }
}
