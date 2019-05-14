using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZTweets_tiny_harness
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Show splash screen
            frmMain main = new frmMain();
            main.Show();
            if (main.Initialise())
            {
                // Run the application, giving it the splash screen to receive messages initially.
                // The main form will take over when the Controller is initialised.
                Application.Run(main);
            }


        }
    }
}
