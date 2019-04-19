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
            frmSplash splash = new frmSplash();
            splash.Show();

            Controller controller = new Controller(splash);
            frmMain main = new frmMain(controller);
            controller.Initialise(main);

            Application.Run(splash);
        }
    }
}
