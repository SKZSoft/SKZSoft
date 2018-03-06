using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets.Data
{
    public class ScheduleComponent
    {
        /// <summary>
        /// Trigger event every X minutes
        /// </summary>
        public int EveryXMinutes { get; set; }

        /// <summary>
        /// Time of first event
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// Time of final event
        /// </summary>
        public DateTime Until { get; set; }


        /// <summary>
        /// The order of this component in the list
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="order"></param>
        /// <param name="everyXMinutes"></param>
        /// <param name="from"></param>
        /// <param name="until"></param>
        public ScheduleComponent(int order, int everyXMinutes, DateTime from, DateTime until)
        {
            Order = order;
            EveryXMinutes = everyXMinutes;
            From = from;
            Until = until;
        }
    }
}
