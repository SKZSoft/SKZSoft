using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;

namespace SKZTweets.Usercontrols
{
    /// <summary>
    /// A line in a Schedule with the editing buttons (add before, add after, remove this)
    /// </summary>
    public partial class ctlScheduleComponentEditor : ctlScheduleComponent
    {
        public ctlScheduleComponentEditor()
        {
            InitializeComponent();
        }

        #region events
        public event EventHandler<EventArgs> Remove;

        /// <summary>
        /// Raise when "Remove" clicked. Container should remove this component.
        /// </summary>
        protected virtual void OnRemove()
        {
            EventHandler<EventArgs> handler = Remove;
            if (handler != null)
            {
                EventArgs e = new EventArgs();
                handler(this, e);
            }
        }



        /// <summary>
        /// Raised when the "Add" button is clicked. Container should add a new component after this one.
        /// </summary>
        protected virtual void OnAddNew(AddScheduleClausePosition position)
        {
            EventHandler<AddScheduleClauseArgs> handler = AddNew;
            if (handler != null)
            {
                AddScheduleClauseArgs e = new AddScheduleClauseArgs(position);
                handler(this, e);
            }
        }

        public event EventHandler<AddScheduleClauseArgs> AddNew;
        #endregion

        /// <summary>
        /// Gives the left position of the "remove" button so that the container can align its controls to it
        /// </summary>
        public int RemoveButtonLeft
        {
            get { return btnRemoveComponent.Left; }
        }

        /// <summary>
        /// Gives the left position of the "add" button so that the container can align its controls to it
        /// </summary>
        public int AddButtonLeft
        {
            get { return btnAddComponentBefore.Left; }
        }

        private void btnRemoveComponent_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // Let parent control know of request to remove me
                OnRemove();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void btnAddComponentBefore_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                
                // Let parent control know of request to add one of me before this instance
                OnAddNew(AddScheduleClausePosition.Before);
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void btnAddComponentAfter_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                // Let parent control know of request to add one of me AFTER this instance
                OnAddNew(AddScheduleClausePosition.After);
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }
    }
}
