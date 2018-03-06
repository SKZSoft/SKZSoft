using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SKZTweets.Data;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;

namespace SKZTweets.Usercontrols
{
    /// <summary>
    /// A complete Schedule from first to last action
    /// </summary>
    public partial class Schedule : UserControl
    {
        public LinkedList<ctlScheduleComponentEditor> m_components;

        /// <summary>
        /// Usercontrol which represents a Schedule.
        /// Has numerous SchedueComponent child controls and can add and remove them.
        /// Issues resize request in form of an event when adding or removing shild controls.
        /// </summary>
        public Schedule()
        {
            InitializeComponent();
        }

        public void Initialise()
        {
            // create new linked list
            m_components = new LinkedList<Usercontrols.ctlScheduleComponentEditor>();
            m_components.AddFirst(ctlScheduleComponentEditor1);
            ctlScheduleComponentEditor1.AddNew += CtlScheduleComponentEditor1_AddNew;
            ctlScheduleComponentEditor1.Remove += CtlScheduleComponentEditor1_Remove;

            // give the existing child control a schedule component to display
            ctlScheduleComponentEditor1.ScheduleComponent = new SKZTweets.Data.ScheduleComponent(0, 1, DateTime.Now, DateTime.Now);
        }

        private void CtlScheduleComponentEditor1_Remove(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // cast sender
                ctlScheduleComponentEditor editor = (ctlScheduleComponentEditor)sender;

                // Can do nothing if it is the only component
                if (m_components.Count == 1)
                {
                    theLog.Log.WriteDebug("Only one component remains. Aborting.", Logging.LoggingSource.GUI);
                    Utils.SKZMessageBox(Strings.LastComponentCannotBeRemoved, MessageBoxIcon.Error);
                    return;
                }

                // Get user to verify
                if (!Utils.SKZConfirmationMessageBox(Strings.RemoveComponent))
                {
                    theLog.Log.WriteDebug("User aborted operation", Logging.LoggingSource.GUI);
                    return;
                }

                // remove control, unhook it, and dispose it.
                theLog.Log.WriteDebug("Removing control", Logging.LoggingSource.GUI);
                m_components.Remove(editor);
                this.Controls.Remove(editor);
                editor.AddNew -= CtlScheduleComponentEditor1_AddNew;
                editor.Remove -= CtlScheduleComponentEditor1_Remove;
                editor.Dispose();

                // respace all controls
                RespaceControls();
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void CtlScheduleComponentEditor1_AddNew(object sender, AddScheduleClauseArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // cast sender
                ctlScheduleComponentEditor editor = (ctlScheduleComponentEditor)sender;

                // which one was it?
                int insertAfter = editor.ScheduleComponent.Order;

                // if we need to insert BEFORE this one then just go back a bit
                if (e.Position == AddScheduleClausePosition.Before)
                {
                    insertAfter -= 1;
                }

                InsertNewComponent(insertAfter);
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Inserts a new schedule component at the specified position
        /// </summary>
        /// <param name="insertAfter">-1 to insert at the top of the list; otherwise the index of the control to insert after</param>
        private void InsertNewComponent(int insertAfter)
        {
            try
            {
                theLog.Log.LevelDown();

                // create new control and add to form
                ctlScheduleComponentEditor newEditor = new Usercontrols.ctlScheduleComponentEditor();
                newEditor.ScheduleComponent = new SKZTweets.Data.ScheduleComponent(insertAfter, 1, DateTime.Now, DateTime.Now);
                this.Controls.Add(newEditor);

                // hook up events
                newEditor.AddNew += CtlScheduleComponentEditor1_AddNew;
                newEditor.Remove += CtlScheduleComponentEditor1_Remove;

                LinkedListNode<ctlScheduleComponentEditor> newNode;

                // assume it's the first control
                int newTop = 0;
                if (insertAfter > -1)
                {
                    // get the actual control
                    ctlScheduleComponentEditor controlAbove = m_components.ElementAt(insertAfter);

                    // insert this one after it
                    // NASTY but we need the NODE and this is the only way to get it.
                    LinkedListNode<ctlScheduleComponentEditor> oldNode = m_components.Find(controlAbove);
                    newNode = m_components.AddAfter(oldNode, newEditor);
                }
                else
                {
                    newNode = m_components.AddFirst(newEditor);
                }

                newEditor.Top = newTop;
                newEditor.Visible = true;

                RespaceControls();
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void RespaceControls()
        {
            try
            {
                theLog.Log.LevelDown();

                // Get first item
                LinkedListNode<ctlScheduleComponentEditor> node = m_components.First;

                // get where it should be positioned
                int newTop = 0;

                // run through all controls, setting them in correct position
                int newOrder = 0;
                int maxWidth = 0;
                while (node != null)
                {
                    // get control
                    ScheduleComponent item = node.Value.ScheduleComponent;

                    // and a casted version and set tab index correctly
                    ctlScheduleComponent control = node.Value;
                    control.TabIndex = newOrder;


                    // set order
                    item.Order = newOrder;
                    newOrder++;

                    // set new top and move top down for next control
                    node.Value.Top = newTop;
                    newTop += node.Value.Height + Consts.ControlSpacing;

                    // track max width
                    int thisWidth = node.Value.Left + node.Value.Width;
                    if (thisWidth > maxWidth)
                    {
                        maxWidth = thisWidth;
                    }

                    // next control
                    node = node.Next;
                }

                // Ask container to resizze to fit all controls snugly.
                DoRequestResize(maxWidth, newTop);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void DoRequestResize(int width, int height)
        {
            try
            {
                theLog.Log.LevelDown();

                ResizedArgs args = new ResizedArgs(width, height);
                OnRequestResize(args);
            }
            finally { theLog.Log.LevelUp(); }
        }

        #region events
        protected virtual void OnRequestResize(ResizedArgs e)
        {
            EventHandler<ResizedArgs> handler = RequestResize;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<ResizedArgs> RequestResize;

        #endregion

    }
}
