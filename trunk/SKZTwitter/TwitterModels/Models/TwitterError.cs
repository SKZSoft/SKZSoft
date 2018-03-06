using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets.TwitterModels
{
    public class TwitterError
    {
        public int code { get; set; }
        public Enums.TwitterErrorCodes CodeAsEnum {  get { return (Enums.TwitterErrorCodes)code;  } }

        public string message { get; set; }
    }
}
