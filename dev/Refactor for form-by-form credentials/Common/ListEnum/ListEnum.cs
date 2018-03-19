using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.ListEnum
{
    /// <summary>
    /// Simple wrapper for displaying enums in GUI list controls
    /// </summary>
    /// <typeparam name="T">The name of the enum to use this class for</typeparam>
    public class ListEnum<T>
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="text"></param>
        public ListEnum(T value, string text)
        {
            Value = value;
            Text = text;
        }

        /// <summary>
        /// The value of the enumeration
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Text to display in combo/listbox etc
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Override for easy display in GUI controls
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return Text; }
    }
}
