using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.SKZTweets.DataModels
{
    [System.Data.Linq.Mapping.Table(Name="TwitterAccount")]
    public class TwitterAccount
    {
        /// <summary>
        /// Dummy constructor to allow Entity Framework to use this class
        /// </summary>
        public TwitterAccount()
        {

        }

        public TwitterAccount(ulong accountId, string screenName, string oAuthToken, string oAuthTokenSecret, Color backColor, Color ForeColor)
        {
            AccountId = accountId;
            ScreenName = screenName;
            OAuthToken = oAuthToken;
            OAuthTokenSecret = oAuthTokenSecret;
            BackColor = backColor;
            ForeColor = ForeColor;
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
        public string ScreenName { get; set; }

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


        public int BackColorRGB
        {
            get { return BackColor.ToArgb(); }
            set { BackColor = Color.FromArgb(value); }
        }

        public int ForeColorRGB
        {
            get { return ForeColor.ToArgb(); }
            set { ForeColor = Color.FromArgb(value); }
        }



        [NotMapped]
        public Color BackColor { get; set; }

        [NotMapped]
        public Color ForeColor { get; set; }

        public override string ToString()
        {
            return ScreenName;
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


        public Credentials Credentials
        {
            get
            {
                Credentials credentials = new Credentials(OAuthToken, OAuthTokenSecret, ScreenName, AccountId);
                return credentials;
            }
        }

    }

}
