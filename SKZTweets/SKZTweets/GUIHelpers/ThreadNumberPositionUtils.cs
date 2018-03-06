using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterData.Enums;
using SKZSoft.Common.ListEnum;

namespace SKZSoft.SKZTweets.GUIHelpers
{
    public static class ThreadNumberPositionUtils
    {

        public static string ToString(ThreadNumberPosition value)
        {
            switch (value)
            {
                case ThreadNumberPosition.NumbersAtEnd:
                    return Strings.ThreadNumberPositionEnd;

                case ThreadNumberPosition.NumbersAtStart:
                    return Strings.ThreadNumberPositionStart;
            }

            return "INVALID VALUE";
        }


        /// <summary>
        /// Return list of ListEnum objects ready to populat a combo or list control with enumeration values
        /// </summary>
        /// <returns></returns>
        public static List<ListEnum<ThreadNumberPosition>> GetThreadNumberPositionOptions()
        {
            List<ListEnum<ThreadNumberPosition>> list = new List<ListEnum<ThreadNumberPosition>>();

            ThreadNumberPosition value;
            string text;

            // End
            value = ThreadNumberPosition.NumbersAtEnd;
            text = ThreadNumberPositionUtils.ToString(value);
            list.Add(new ListEnum<ThreadNumberPosition>(value, text));

            // Start
            value = ThreadNumberPosition.NumbersAtStart;
            text = ThreadNumberPositionUtils.ToString(value);
            list.Add(new ListEnum<ThreadNumberPosition>(value, text));

            return list;
        }
    }
}
