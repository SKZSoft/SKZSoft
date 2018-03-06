using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.SKZTweets.Controllers
{
    public class Scheduler
    {

        public Queue<DateTime> GetScheduleTimes(DateTime startAt, DateTime endAt, int intervalMinutes)
        {
            DateTime today = DateTime.Now.Date;
            DateTime mark = today.AddHours(startAt.Hour);
            mark = mark.AddMinutes(startAt.Minute);

            // special case: if end at before StartAt, then treat it as tomorrow
            if(endAt < startAt)
            {
                endAt = endAt.AddDays(1);
            }

            Queue<DateTime> values = new Queue<DateTime>();
            while(mark < endAt)
            {
                values.Enqueue(mark);
                mark = mark.AddMinutes(intervalMinutes);
            }

            return values;
        }

    }
}

