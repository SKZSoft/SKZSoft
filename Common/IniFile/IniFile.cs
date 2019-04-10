using System;
using System.Collections.Generic;
using System.IO;

namespace SKZSoft.Common.IniFile
{
    public class IniFile
    {
        string m_path;
        Dictionary<string, string> m_items = new Dictionary<string, string>();

        public IniFile(string path)
        {
            m_path = path;
            LoadFile();
        }


        private void LoadFile()
        {
            // read all file
            string allText = File.ReadAllText(m_path);


            // split into lines (ignoring blank lines)
            string[] lines = allText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // extract key value pairs.
            // no validation or safety - invalid entires will throw an exception
            // TODO: sanitise.
            foreach (string line in lines)
            {
                string[] kvp = line.Split(new string[] { "=" }, StringSplitOptions.None);
                m_items.Add(kvp[0].ToLower(), kvp[1]);
            }
        }

        public string GetEntry(string key)
        {
            key = key.ToLower();
            if (m_items.ContainsKey(key))
            {
                return m_items[key];
            }

            return String.Empty;
        }

        /// <summary>
        /// Returns the INI entry as a ulong
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ulong GetEntryAsUlong(string key)
        {
            string entry = GetEntry(key);
            ulong value = 0;
            if (ulong.TryParse(entry, out value))
            {
                return value;
            }

            return 0;
        }

    }
}
