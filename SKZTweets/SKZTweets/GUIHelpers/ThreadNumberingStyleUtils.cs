using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterData.Enums;
using SKZSoft.Common.ListEnum;

namespace SKZSoft.SKZTweets.GUIHelpers
{
    public static class ThreadNumberingStyleUtils
    {
        public static string ToString(ThreadNumberStyle value)
        {
            switch (value)
            {
                case ThreadNumberStyle.X:
                    return Strings.ThreadNumberingStyleX;

                case ThreadNumberStyle.XofY:
                    return Strings.ThreadNumberingStyleXofY;

                case ThreadNumberStyle.NoNumbers:
                    return Strings.ThreadNumberingStyleNoNumbers;
            }

            return "INVALID VALUE";
        }

        /// <summary>
        /// Return list of ListEnum objects ready to populat a combo or list control with enumeration values
        /// </summary>
        /// <returns></returns>
        public static List<ListEnum<ThreadNumberStyle>> GetThreadNumberingStyleOptions()
        {
            List<ListEnum<ThreadNumberStyle>> list = new List<ListEnum<ThreadNumberStyle>>();

            ThreadNumberStyle value;
            string text;

            // X
            value = ThreadNumberStyle.X;
            text = ThreadNumberingStyleUtils.ToString(value);
            list.Add(new ListEnum<ThreadNumberStyle>(value, text));

            // X of Y
            value = ThreadNumberStyle.XofY;
            text = ThreadNumberingStyleUtils.ToString(value);
            list.Add(new ListEnum<ThreadNumberStyle>(value, text));

            // No numbers
            value = ThreadNumberStyle.NoNumbers;
            text = ThreadNumberingStyleUtils.ToString(value);
            list.Add(new ListEnum<ThreadNumberStyle>(value, text));

            return list;
        }
    }
}
