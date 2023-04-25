using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZSoft.SKZTweets.Data;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;

namespace SKZSoft.SKZTweets.Usercontrols
{
    /// <summary>
    /// A single component of a scheuled (sart/end/every)
    /// </summary>
    public partial class ctlScheduleComponent : UserControl
    {
        private ScheduleComponent m_scheduleComponent;
        
        public ctlScheduleComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set control to match the specified schedule component
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // do NOT serialise. Run-time only.
        public ScheduleComponent ScheduleComponent
        {
            get { return m_scheduleComponent; }
            set
            {
                try
                {
                    theLog.Log.LevelDown();
                    m_scheduleComponent = value;
                    if (m_scheduleComponent != null)
                    {
                        txtMinutes.Text = m_scheduleComponent.EveryXMinutes.ToString();
                        dtpStartTime.Value = m_scheduleComponent.From;
                        dtpEndTime.Value = m_scheduleComponent.Until;
                    }
                }
                finally { theLog.Log.LevelUp(); }
            }
        }

    }
}
