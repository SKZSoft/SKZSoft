using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZSoft.SKZTweets.Usercontrols
{
    public partial class ctlScheduleBasic : UserControl
    {
        public ctlScheduleBasic()
        {
            InitializeComponent();
        }

        public int IntervalMinutes
        {
            get
            {
                int value = 0;
                int.TryParse(txtInterval.Text, out value);
                return value;
            }
            set
            {
                txtInterval.Text = value.ToString();
            }
        }

        public DateTime StartAt
        {
            get
            {
                DateTime value;
                DateTime.TryParse(txtStartAt.Text, out value);
                return value;
            }
            set
            {
                txtStartAt.Text = value.ToString("HH:mm");
            }
        }

        public DateTime EndAt
        {
            get
            {
                DateTime value;
                DateTime.TryParse(txtEndAt.Text, out value);
                return value;
            }
            set
            {
                txtEndAt.Text = value.ToString("HH:mm");
            }
        }

        public bool Validate(out List<string> errors, bool allowPastTimes)
        {
            errors = new List<string>();

            // start time is a valid time
            DateTime startDate;
            if (!DateTime.TryParse(txtStartAt.Text, out startDate))
            {
                errors.Add(Strings.TimeNotValidStart);
            }

            // interval is an integer
            int tempInt;
            if(!int.TryParse(txtInterval.Text, out tempInt))
            {
                errors.Add(Strings.IntervalMustBeNumeric);
            }

            // end time is a valid time
            DateTime endDate;
            if (!DateTime.TryParse(txtEndAt.Text, out endDate))
            {
                errors.Add(Strings.TimeNotValidStart);
            }


            // times are not all in the past
            if(!allowPastTimes)
            {
                DateTime now = DateTime.Now;

                if(now > startDate && now > endDate)
                {
                    errors.Add(Strings.ScheduleIsInThePast);
                }
            }

            return (errors.Count == 0);
        }

    }
}
