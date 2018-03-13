using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.SKZTweets.Data
{
    public class SendDMData
    {

        public ulong RecipientId { get; set; }
        public string Text { get; set; }

        public SendDMData(ulong recipientID, string text)
        {
            RecipientId = recipientID;
            Text = text;
        }
    }
}
