using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;

namespace SKZSoft.SKZTweets.DataModels
{
    [Table(Name="Users")]
    public class User
    {
        /*
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="screenName"></param>
        /// <param name="oAuthToken"></param>
        /// <param name="oAuthTokenSecret"></param>
        public User(ulong userId, string screenName, string oAuthToken, string oAuthTokenSecret)
        {
            UserId = userId;
            Screenname = screenName;
            OAuthToken = oAuthToken;
            OAuthTokenSecret = oAuthTokenSecret;
        }
        TODO - remove above */

        /// <summary>
        /// Unique twitter ID
        /// </summary>
        [Key]
        public ulong UserId { get; set; }

        /// <summary>
        /// Screenname. May change; UserID will not.
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
    }

}
