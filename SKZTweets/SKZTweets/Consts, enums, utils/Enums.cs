using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.SKZTweets
{
    /// <summary>
    /// Returned by a form which is queried as to whether or not it can close.
    /// </summary>
    public enum FormCloseAction
    {
        /// <summary>
        /// Do not close the form after all
        /// </summary>
        CancelClose,

        /// <summary>
        /// Close this window AND close all other windows
        /// </summary>
        CloseAllWindows,

        /// <summary>
        /// It is OK to close the form.
        /// </summary>
        CloseOK
    }
}
