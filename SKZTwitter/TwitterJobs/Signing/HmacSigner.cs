using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SKZSoft.Twitter.TwitterJobs.Signing
{

    /// <summary>
    /// Implements RFC 2104. Generates a signature.
    /// </summary>
    public class HmacSigner
    {
        private const int BLOCKSIZE = 64;


        public byte[] Sign(string key, string msg)
        {
            // convert strings to byte arrays for the heavy lifting
            byte[] byteKey = Encoding.UTF8.GetBytes(key);
            byte[] byteMsg = Encoding.UTF8.GetBytes(msg);

            // initialize key to be exactly 64 bytes
            byte[] hashKey = InitializeKeyToCorrectLength(byteKey);

            HMAC hmac = new HMACSHA1();
            hmac.Key = hashKey;
            byte[] signature = hmac.ComputeHash(byteMsg);

            return signature;
        }

        /// <summary>
        /// Pad a key to be the exact BLOCKSIZE
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] GetPaddedKey(byte[] key)
        {
            byte[] paddedKey = new byte[BLOCKSIZE];     // arrays are initialized to be full of zeros (source: c# language spec)
            Array.Copy(key, paddedKey, key.Length);
            return paddedKey;
        }

        /// <summary>
        /// Make sure the key is exactly the same size as BLOCKSIZE
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] InitializeKeyToCorrectLength(byte[] key)
        {
            // fast get-out if correct size already.
            if(key.Length == BLOCKSIZE)
            {
                return key;
            }

            byte[] keyToUse;
            if (key.Length > BLOCKSIZE)
            {
                // Key is too long. Hash it to make it small enough.
                SHA1 hasher = new SHA1CryptoServiceProvider();
                keyToUse = hasher.ComputeHash(key);
            }
            else
            {
                keyToUse = key;
            }
    
            // pad to 64 bytes
            keyToUse = GetPaddedKey(keyToUse);

            return keyToUse;
        }
    }
}
