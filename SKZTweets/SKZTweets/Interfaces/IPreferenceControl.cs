using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SKZTweets.Data_Classes.Preferences;

namespace SKZTweets
{
    /// <summary>
    /// Standard interface for a control which is part of the "preference" interface
    /// </summary>
    public interface IPreferenceControl
    {
        /// <summary>
        /// Perform validation of data.
        /// </summary>
        /// <param name="errors">A list of string. Errors are added to the list for display by the caller.</param>
        /// <param name="setFocusToFirstError">TRUE if control should set the focus to the first control with an error</param>
        /// <returns></returns>
        bool Validate(List<string> errors, bool setFocusToFirstError);

        /// <summary>
        /// TRUE if data on the control has changed
        /// </summary>
        /// <returns></returns>
        bool IsDirty { get; }

        /// <summary>
        /// Read preferences from the class structure provided
        /// </summary>
        /// <param name="prefs"></param>
        void ReadPreferences(Preferences prefs);

        /// <summary>
        /// Write preferences to the class structure provided
        /// </summary>
        /// <param name="prefs"></param>
        void WritePreferences(Preferences prefs);
    }
}
