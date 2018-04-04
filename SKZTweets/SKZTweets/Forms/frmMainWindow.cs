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
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Reflection;
using SKZSoft.Twitter.TwitterData;
using System.IO;
using SKZSoft.SKZTweets.Interfaces;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.SKZTweets.DataModels;

namespace SKZSoft.SKZTweets
{
    public partial class frmMainWindow : Form, IMainWindow
    {
        private AppController m_mainController;
        private TwitterAccount m_selectedAccount;
        private DataBase.Persistence m_persistence;
        private List<TwitterAccount> m_twitterAccounts;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mainController">Main AppController object (initialised)</param>
        public frmMainWindow(AppController mainController, DataBase.Persistence persistence, TwitterAccount account)
        {
            try
            {
                theLog.Log.LevelDown();
                InitializeComponent();

                m_mainController = mainController;
                m_persistence = persistence;
                SetFormText();

                List<TwitterAccount> twitterAccounts = m_persistence.TwitterAccountGetAllAvailable();
                UpdateTwitterAccounts(twitterAccounts, account);
                m_twitterAccounts = twitterAccounts;


                schedulesToolStripMenuItem.Visible = false;
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void UpdateTwitterAccounts(List<TwitterAccount> twitterAccounts, TwitterAccount selectedAccount)
        {
            tscTwitterAccount.Items.Clear();
            foreach(TwitterAccount ta in twitterAccounts)
            {
                int index = tscTwitterAccount.Items.Add(ta);
                if(ta.AccountId == selectedAccount.AccountId)
                {
                    tscTwitterAccount.SelectedIndex = index;
                }
            }

            m_selectedAccount = selectedAccount;
        }



        /// <summary>
        /// Prepare window for initial display
        /// </summary>
        public void Initialise()
        {
            try
            {
                theLog.Log.LevelDown();

                SetMenusEnabled();
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Set form's caption to be app name, version and sign-in name
        /// </summary>
        private void SetFormText()
        {
            try
            {
                string appName = string.Format("{0} v{1}", Strings.AppName, typeof(frmMainWindow).Assembly.GetName().Version);
                this.Text = appName;
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// File/Exit
        /// Try to close everything down (can be cancelled)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                this.Close();       // child forms will decide if this should be cancelled
            }
            finally { theLog.Log.LevelUp(); }

        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                theLog.Log.LevelDown();

                if (disposing)
                {
                    if (components != null)
                    {
                        components.Dispose();
                    }
                    m_mainController = null;
                }
                base.Dispose(disposing);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void threadCreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                DoThreadCreator();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void DoThreadCreator()
        {
            try
            {
                theLog.Log.LevelDown();
                frmThreadCreator creator = new frmThreadCreator(m_twitterAccounts, m_selectedAccount, m_mainController);
                creator.MdiParent = this;
                creator.Show();
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void SetMenusEnabled()
        {
            try
            {
                theLog.Log.LevelDown();

                // TODO - decide if we allow any situation in which there is no account.
                // Will depend on account removal logic - can all accounts be removed?
                bool valid = (m_selectedAccount != null);
                theLog.Log.WriteDebug("valid = " + valid.ToString(), Logging.LoggingSource.GUI);

                retweeterToolStripMenuItem.Enabled = valid;
                tsbRetweeter.Enabled = valid;

                threadCreatorToolStripMenuItem.Enabled = valid;
                tsbThreadCreator.Enabled = valid;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void retweeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                DoRetweeter();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }




        private bool CloseAllChildWindows()
        {
            try
            {
                theLog.Log.LevelDown();
                // Close all windows (remaining safe and allowing operation to cancel)
                FormCloseAction action = FormCloseAction.CloseOK;
                Form[] forms = this.MdiChildren;

                m_mainController.AllFormsMayClose = false;

                foreach (SafeForm form in forms)
                {
                    // verify if we haven't authority to close everything
                    if (action != FormCloseAction.CloseAllWindows)
                    {
                        // ask the user
                        action = Utils.QueryFormClose(form, m_mainController);

                        // bomb out if user cancelled
                        if (action == FormCloseAction.CancelClose)
                        {
                            return false;
                        }
                    }
                }

                // user has given permission for everything to close
                foreach (SafeForm form in forms)
                {
                    form.ForceClose();
                    this.RemoveOwnedForm(form);
                }

                m_mainController.AllFormsMayClose = false;
                return true;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void frmMainWindow_Load(object sender, EventArgs e)
        {

        }

        private void tsbRetweeter_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                DoRetweeter();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void DoRetweeter()
        {
            theLog.Log.LevelDown();
            try
            {
                frmRetweeter retweeter = new frmRetweeter(m_twitterAccounts, m_selectedAccount, m_mainController);
                retweeter.MdiParent = this;
                retweeter.Show();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally
            {
                theLog.Log.LevelUp();
            }
        }



        private void tsbThreadCreator_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                DoThreadCreator();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                Forms.frmScheduleEditor editor = new SKZTweets.Forms.frmScheduleEditor();
                editor.MdiParent = this;
                editor.Show();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                frmPreferences preferences = new frmPreferences();
                preferences.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void openLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                string path = theLog.Log.LogPath;
                theLog.Log.WriteDebug(string.Format("Opening Log file at {0}", path), Logging.LoggingSource.GUI);
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void loggingSettingsTtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelUp();
                Logging.LogSettings settings = theLog.Log.LogSettings;

                Logging.GUI.EditSettings editor = new Logging.GUI.EditSettings();
                Logging.LogSettings newSettings = editor.ShowForm(settings);

                if (newSettings == null)
                {
                    return;
                }

                // re-initialise log
                Logging.Logger.Initialise(newSettings);

                // persist these settings to app settings
                Properties.Settings appSettings = Properties.Settings.Default;
                appSettings.LogDeleteAfterDays = settings.DeleteAfterDays;
                appSettings.LogLevel = (int)settings.Level;
                appSettings.Save();

            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void openUnhandledLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                string path = theLog.Log.UnhandledLogPath;

                if(!File.Exists(path))
                {
                    string msg = string.Format(Strings.FileDoesNotExist, path);
                    Utils.SKZMessageBox(msg, MessageBoxIcon.Stop);
                    return;
                }

                theLog.Log.WriteDebug(string.Format("Opening Log file at {0}", path), Logging.LoggingSource.GUI);
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }

        }

        private void openLogDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                string path = theLog.Log.LogDirectory;

                if (!Directory.Exists(path))
                {
                    string msg = string.Format(Strings.LogDirectoryDoesNotExist, path);
                    Utils.SKZMessageBox(msg, MessageBoxIcon.Stop);
                    return;
                }

                theLog.Log.WriteDebug(string.Format("Opening Log directory at {0}", path), Logging.LoggingSource.GUI);
                System.Diagnostics.Process.Start(path);

            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void viewChangelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                m_mainController.ShowChangeLog();
            }
            catch(Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void viewKnownIssuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                m_mainController.ShowKnownIssues();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }

        }

        private void frmMainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_mainController.Terminate();
            m_mainController = null;
        }

        private void frmMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // system is demanding we close. Do not argue.
            if(e.CloseReason == CloseReason.TaskManagerClosing || e.CloseReason == CloseReason.WindowsShutDown)
            {
                return;
            }

            // ask child forms and close them one by one - if they allow it.
            Form[] forms = this.MdiChildren;
            foreach(SafeForm form in forms)
            {
                FormCloseAction action = form.QueryTerminate();

                // Cancel if instructed to do so
                if(action == FormCloseAction.CancelClose)
                {
                    e.Cancel = true;
                    return;
                }

                // if OK to close all forms, flag that up.
                if(action == FormCloseAction.CloseAllWindows)
                {
                    m_mainController.AllFormsMayClose = true;
                }

                // close the form
                form.Close();
            }
        }

        private void tsbFollowerMaint_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                DoFollowersMaintenence();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void DoFollowersMaintenence()
        {
            try
            {
                theLog.Log.LevelDown();
                frmFollowersMaintenence form = new frmFollowersMaintenence(m_twitterAccounts, m_selectedAccount, m_mainController);
                form.MdiParent = this;
                form.Show();
            }
            finally { theLog.Log.LevelUp(); }

        }
    }
}
