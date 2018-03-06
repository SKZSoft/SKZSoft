using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterModels
{
    public class TwitterError
    {
        public int code { get; set; }
        public Enums.TwitterErrorCodes CodeAsEnum {  get { return (Enums.TwitterErrorCodes)code;  } }

        public string message { get; set; }
    }
}
