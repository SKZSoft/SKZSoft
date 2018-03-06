using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.SKZTweets.Data_Classes.Preferences
{
    /// <summary>
    /// The user-preferences for the application
    /// </summary>
    public class Preferences
    {
        private PrefsSchedule m_schedule = new PrefsSchedule();

        public PrefsSchedule Schedule {  get { return m_schedule;  } }

    }
}
