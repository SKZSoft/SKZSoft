using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZSoft.Twitter.TwitterData.Enums;
using SKZSoft.Common.ListEnum;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterData.Models;

namespace SKZSoft.SKZTweets.Usercontrols
{
    public partial class ctlTweetNumbering : UserControl
    {
        private bool m_raiseChanged = true;
        private bool m_initialised = false;

        public ctlTweetNumbering()
        {
            InitializeComponent();
        }

        public void PopulateControls(ThreadNumberPosition selectedPosition, ThreadNumberStyle selectedStyle)
        {
            m_raiseChanged = false;

            // Thread numbering style
            List<ListEnum<ThreadNumberStyle>> styles = SKZTweets.GUIHelpers.ThreadNumberingStyleUtils.GetThreadNumberingStyleOptions();
            ListEnum<ThreadNumberStyle> selectedStyleItem = null;
            foreach (ListEnum<ThreadNumberStyle> style in styles)
            {
                cmbNumberStyle.Items.Add(style);
                if (style.Value == selectedStyle)
                {
                    selectedStyleItem = style;
                }
            }

            if (selectedStyleItem != null)
            {
                cmbNumberStyle.SelectedItem = selectedStyleItem;
            }

            ListEnum<ThreadNumberPosition> selectedPositionItem = null;
            List<ListEnum<ThreadNumberPosition>> positions = SKZTweets.GUIHelpers.ThreadNumberPositionUtils.GetThreadNumberPositionOptions();
            foreach (ListEnum<ThreadNumberPosition> position in positions)
            {
                cmbNumberPosition.Items.Add(position);
                if (position.Value == selectedPosition)
                {
                    selectedPositionItem = position;
                }
            }

            if (selectedPositionItem != null)
            {
                cmbNumberPosition.SelectedItem = selectedPositionItem;
            }

            m_raiseChanged = true;
            m_initialised = true;
        }


        /// <summary>
        /// Raised whenever data in the control is changed
        /// </summary>
        public event EventHandler<NumberingChangedArgs> SettingsChanged;

        protected virtual void OnSettingsChanged()
        {
            try
            {
                theLog.Log.LevelDown();
                if (!m_raiseChanged)
                {
                    return;
                }

                EventHandler<NumberingChangedArgs> handler = SettingsChanged;
                if (handler != null)
                {
                    ThreadNumberSettings settings = GetSettings();
                    NumberingChangedArgs e = new NumberingChangedArgs(settings);
                    handler(this, e);
                }
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Get settings from control
        /// </summary>
        /// <returns></returns>
        public ThreadNumberSettings GetSettings()
        {
            ThreadNumberSettings settings = new ThreadNumberSettings(Style, Position);
            return settings;
        }


        /// <summary>
        /// The style of numbering
        /// </summary>
        public ThreadNumberStyle Style
        {
            get
            {
                ListEnum<ThreadNumberStyle> item = (ListEnum < ThreadNumberStyle > )cmbNumberStyle.SelectedItem;
                return item.Value;
            }
        }


        /// <summary>
        /// The numbering position
        /// </summary>
        public ThreadNumberPosition Position
        {
            get
            {
                ListEnum<ThreadNumberPosition> item = (ListEnum < ThreadNumberPosition > )cmbNumberPosition.SelectedItem;
                return item.Value;
            }
        }



        private void txtAfterNumber_TextChanged(object sender, EventArgs e)
        {
            if (!m_initialised)
            {
                return;
            }

            try
            {
                theLog.Log.LevelDown();
                OnSettingsChanged();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void cmbNumberStyle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!m_initialised)
            {
                return;
            }

            try
            {
                theLog.Log.LevelDown();

                OnSettingsChanged();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void cmbNumberPosition_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!m_initialised)
            {
                return;
            }

            try
            {
                theLog.Log.LevelDown();
                OnSettingsChanged();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }
    }
}
