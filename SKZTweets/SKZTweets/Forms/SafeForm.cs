using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZSoft.SKZTweets.Controllers;
using SKZSoft.SKZTweets.Interfaces;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;

namespace SKZSoft.SKZTweets
{
    /// <summary>
    /// Form from which other forms inherit.
    /// Implements the functionality to allow Dirty, Busy, and prompting user to close without saving changes
    /// </summary>
    public partial class SafeForm : Form
    {
        protected AppController m_mainController;

        /// <summary>
        /// Constructor
        /// </summary>
        public SafeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The main controller.
        /// </summary>
        public AppController MainController { set { m_mainController = value; } }

        /// <summary>
        /// Set to TRUE if the user has made changes which should flag a warning
        /// </summary>
        public virtual bool Dirty { get { return false; } }

        /// <summary>
        /// Set to TRUE if the form is currently doing something
        /// </summary>
        public virtual bool Busy { get { return false; } }


        /// <summary>
        /// Handle all actions allowing form to terminate.
        /// Asks user if they wish to close, close all, or cancel whatever operation started the query
        /// </summary>
        /// <returns></returns>
        public FormCloseAction QueryTerminate()
        {
            return Utils.QueryFormClose(this, m_mainController);
        }


        /// <summary>
        /// Force this form to close, no questions asked.
        /// </summary>
        public void ForceClose()
        {
            this.Close();
        }


        private void SafeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // Tidy up nicely. Child forms override Terminate.
                Terminate();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Clean up the form.
        /// Inheriting forms should override this.
        /// This form should ideally be abstract but that causes errors in the IDE.
        /// </summary>
        public virtual void Terminate() { }
    }
}
