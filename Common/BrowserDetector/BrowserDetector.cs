using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Collections;

namespace SKZSoft.Common.BrowserDetector
{
    public class BrowserDetector
    {
        private SortedList m_browsers;

        public SortedList Items {  get { return m_browsers;  } }

        public BrowserDetector()
            : this(false, "") { }

        public BrowserDetector(bool addUseDefault, string useDefaultText)
        {
            try
            {
                m_browsers = new SortedList();
                RegistryKey mainKey = Registry.LocalMachine.OpenSubKey(@"Software\Clients\StartMenuInternet");
                string defaultId = mainKey.GetValue("").ToString();

                foreach (string browserEntry in mainKey.GetSubKeyNames())
                {
                    RegistryKey browserKey = mainKey.OpenSubKey(browserEntry);
                    string displayName = browserKey.GetValue(null).ToString();
                    RegistryKey openCommand = browserKey.OpenSubKey(@"shell\open\command");
                    string command = openCommand.GetValue(null).ToString();

                    string thisId = browserEntry.ToString();

                    // This doesn't work. The default value of the parent key is always "IEXPLORE".
                    //bool isDefault = (defaultId == thisId);

                    Browser browser = new Browser(thisId, displayName, command);
                    m_browsers.Add(displayName, browser);
                }

                if(addUseDefault)
                {
                    Browser useDefault = GetDefaultBrowser(useDefaultText);
                    m_browsers.Add(useDefault.Id, useDefault);
                }
            }
            catch
            {
                Browser b = new Browser("", "ERROR GETTING BROWSER DATA", "ERROR");
            }
        }

        public Browser GetDefaultBrowser(string useDefaultText)
        {
            try
            {
                RegistryKey mainKey = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command");
                string command = mainKey.GetValue(null).ToString().ToLower();

                // Command has quotes and command-line arguments which must be stripped.
                command = command.Replace("\"", "");

                // strip to the end of "exe"
                int indexExe = command.IndexOf(".exe");

                // all bets are off. 
                if(indexExe<1)
                {
                    return null;
                }

                command = command.Substring(0, indexExe + 4);
                
                // exe name is the first item
                Browser b = new Browser(Consts.DEFAULT_ID, useDefaultText, command);
                return b;

            }
            catch
            {
                Browser b = new Browser("", "ERROR GETTING DEFAULT BROWSER DATA", "ERROR");
                return b;
            }
        }
    }
}
