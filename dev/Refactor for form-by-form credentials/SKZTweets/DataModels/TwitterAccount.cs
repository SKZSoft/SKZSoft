using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace SKZSoft.SKZTweets.DataModels
{
    [Table(Name="TwitterAccount")]
    public class TwitterAccount
    {
        /// <summary>
        /// Dummy constructor to allow Entity Framework to use this class
        /// </summary>
        public TwitterAccount()
        {

        }

        public TwitterAccount(ulong accountId, string screenName, string oAuthToken, string oAuthTokenSecret)
        {
            AccountId = accountId;
            Screenname = screenName;
            OAuthToken = oAuthToken;
            OAuthTokenSecret = oAuthTokenSecret;
        }

        /// <summary>
        /// Unique twitter ID
        /// </summary>
        [Key]
        public ulong AccountId { get; set; }

        /// <summary>
        /// Screenname. May change; AccountId will not.
        /// </summary>
        [MaxLength(100)]
        public string Screenname { get; set; }

        /// <summary>
        /// Twitter authentication data
        /// </summary>
        [MaxLength(100)]
        public string OAuthToken { get; set; }

        /// <summary>
        /// Twitter authentication data
        /// </summary>
        [MaxLength(100)]
        public string OAuthTokenSecret { get; set; }


        public Color BackColor { get; set; }

        public Color ForeColor { get; set; }

        public override string ToString()
        {
            return Screenname;
        }

        public override bool Equals(object obj)
        {
            TwitterAccount cast = obj as TwitterAccount;
            if(cast==null)
            {
                return false;
            }
            return AccountId == cast.AccountId;
        }


        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

}
