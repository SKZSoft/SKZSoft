using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Net.Http;

namespace SKZSoft.Twitter.TwitterJobs.Signing
{
    /// <summary>
    /// Based on the Linq2Twitter class of the same name
    /// </summary>
    public class OAuth
    {

        /// <summary>
        /// If set to TRUE, the timestamp and Guid are mocked, to allow unit testing.
        /// Not setting this would cause the result to change every time the tests are run.
        /// </summary>
        public bool MOCKing { get; set; }


        /// <summary>
        /// Get authorisation string for Twitter request.
        /// This is complex and requires processing all the paramaters in alphabetical order, then hashing them.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="oAuthTokenSecret"></param>
        /// <returns></returns>
        public string GetAuthorizationString(string method, string url, IDictionary<string, string> parameters, string consumerSecret, string oAuthTokenSecret, bool oauthParamsOnly)
        {
            string encodedAndSortedString = BuildEncodedSortedString(parameters, oauthParamsOnly);
            string signatureBaseString = BuildSignatureBaseString(method, url, encodedAndSortedString);
            string signingKey = BuildSigningKey(consumerSecret, oAuthTokenSecret);
            string signature = CalculateSignature(signingKey, signatureBaseString);
            string authorizationHeader = BuildAuthorizationHeaderString(encodedAndSortedString, signature);

            return authorizationHeader;
        }

        /// <summary>
        /// Add any parameters which are required for Auth but are not yet present
        /// </summary>
        /// <param name="parameters"></param>
        internal void AddMissingOAuthParameters(IDictionary<string, string> parameters)
        {
            const string OAuthVersion = "1.0";
            const string OAuthSignatureMethod = "HMAC-SHA1";

            if (!parameters.ContainsKey("oauth_timestamp"))
            {
                string timestamp = GetTimestamp();
                parameters.Add("oauth_timestamp", timestamp);
            }

            if (!parameters.ContainsKey("oauth_nonce"))
            {
                string nonce = GenerateNonce();
                parameters.Add("oauth_nonce", nonce);
            }

            if (!parameters.ContainsKey("oauth_version"))
            {
                parameters.Add("oauth_version", OAuthVersion);
            }

            if (!parameters.ContainsKey("oauth_signature_method"))
            {
                parameters.Add("oauth_signature_method", OAuthSignatureMethod);
            }
        }

        /// <summary>
        /// Build the URL parameter section
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal string BuildEncodedSortedString(IDictionary<string, string> parameters, bool oauthParamsOnly)
        {
            // Add any missing parameters
            AddMissingOAuthParameters(parameters);

            // encode the parameters and seperate out keys and values
            string[] keys = new string[parameters.Count];
            string[] values = new string[parameters.Count];
            int itemIndex = 0;
            foreach (KeyValuePair<string, string> kvp in parameters)
            {
                // only add if we are using ALL parameters or if it's an oauth parameter
                if (!oauthParamsOnly || kvp.Key.StartsWith("oauth"))
                {
                    keys[itemIndex] = kvp.Key;
                    string escapedValue = string.Empty;
                    if (!string.IsNullOrEmpty(kvp.Value))
                    {
                        escapedValue = Uri.EscapeDataString(kvp.Value);
                    }
                    values[itemIndex] = escapedValue;
                    itemIndex++;
                }
            }

            // resize if any parameters were missed
            if (itemIndex < parameters.Count)
            {
                Array.Resize(ref keys, itemIndex);
                Array.Resize(ref values, itemIndex);
            }

            // sort the items
            Array.Sort(keys, values);

            // now put them into a parameter string.
            StringBuilder sb = new StringBuilder(500);
            bool first = true;
            itemIndex = 0;
            foreach (string key in keys)
            {
                if (!first)
                {
                    sb.Append("&");
                }
                sb.Append(string.Format("{0}={1}", key, values[itemIndex]));
                first = false;
                itemIndex++;
            }
            string header = sb.ToString();

            return header;
                
        }

        /// <summary>
        /// Build signature base key according to Twitter specifications
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="encodedStringParameters"></param>
        /// <returns></returns>
        internal string BuildSignatureBaseString(string method, string url, string encodedStringParameters)
        {

            string urlWithoutParams = url;

            int paramsIndex = url.IndexOf('?');
            if (paramsIndex >-1)
            {
                // we have parameters. Chop them off.
                urlWithoutParams = url.Substring(0, paramsIndex);
            }

            method = method.ToUpper();
            string encodedUrl = Uri.EscapeDataString(urlWithoutParams);
            string encodedParams = Uri.EscapeDataString(encodedStringParameters);
            
            string result = string.Format("{0}&{1}&{2}", method, encodedUrl, encodedParams);

            return result;
        }

        internal string BuildSigningKey(string consumerSecret, string oAuthTokenSecret)
        {
            return string.Format(
                CultureInfo.InvariantCulture, "{0}&{1}",
                Uri.EscapeDataString(consumerSecret),
                Uri.EscapeDataString(oAuthTokenSecret));
        }

        internal string CalculateSignature(string signingKey, string signatureBaseString)
        {
            HmacSigner hmac = new HmacSigner();
            byte[] hash = hmac.Sign(signingKey, signatureBaseString);

            return Convert.ToBase64String(hash);
        }


        /// <summary>
        /// Sorts a string with URL parameters into two arrays: one of keys, one of values.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        private void SortParameters(string parameters, out string[] keys, out string[] values)
        {
            string[] allParms = parameters.Split('&');
            keys = new string[allParms.Length];
            values = new string[allParms.Length];

            // Split the parameters into two arrrays: one with keys, the other with values.
            int itemIndex = 0;
            foreach (string item in allParms)
            {
                string[] parts = item.Split('=');
                keys[itemIndex] = parts[0];
                values[itemIndex] = parts[1];
                itemIndex++;
            }

            // sort the parameters
            Array.Sort(keys, values);
        }

        /// <summary>
        /// Get the Twitter authorization header.
        /// Basically, any auth parameters, sorted into alphabetical order, with a prefix.
        /// Get it wrong and Twitter will report an authenication error
        /// </summary>
        /// <param name="encodedAndSortedString"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        internal string BuildAuthorizationHeaderString(string encodedAndSortedString, string signature)
        {
            string encodedSig = Uri.EscapeDataString(signature);
            string combinedUrl = string.Format("{0}&oauth_signature={1}", encodedAndSortedString, encodedSig);

            string[] keys;
            string[] values;
            SortParameters(combinedUrl, out keys, out values);

            StringBuilder sb = new StringBuilder(500);
            bool first = true;
            sb.Append("OAuth ");
            int itemIndex = 0;
            foreach(string key in keys)
            {
                if(key.StartsWith("oauth") || key.StartsWith("x_auth"))
                {
                    if(!first)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(string.Format("{0}=\"{1}\"", key, values[itemIndex]));
                    first = false;
                }
                itemIndex++;
            }
            string header = sb.ToString();

            return header;
        }

        internal string GetTimestamp()
        {
            const long UnixEpocTicks = 621355968000000000L;

            // Fast get-out if unit tests are running.
            if (MOCKing)
            {
                return "1234567890";
            }

            long ticksSinceUnixEpoc = DateTime.UtcNow.Ticks - UnixEpocTicks;
            double secondsSinceUnixEpoc = new TimeSpan(ticksSinceUnixEpoc).TotalSeconds;
            return Math.Floor(secondsSinceUnixEpoc).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Generate a guaranteed unique ID for this request
        /// </summary>
        /// <returns></returns>
        internal string GenerateNonce()
        {
            // if MOCKing, return the same data every time, to allow unit tests to run
            if(MOCKing)
            {
                return "759804569087459568y489067y7y689243790847";
            }

            // Create a GUID for uniqueness
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
    }
}
