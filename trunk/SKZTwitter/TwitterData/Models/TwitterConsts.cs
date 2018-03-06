using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets.TwitterData.Models
{
    /// <summary>
    /// Data which is defined by Twitter.
    /// This is a CLASS so it can be re-used outseid of this library.
    /// </summary>
    public class TwitterConsts
    {

        private const int MAX_IMAGE_FILE_SIZE = 5000000;
        private const int MAX_IMAGES_PER_STATUS = 4;

        /// <summary>
        /// Returns a list of extensions wwhich are valid file extensions for pictures.
        /// Lowercase letters only, eg "jpg", "png"
        /// </summary>
        /// <returns></returns>
        public List<string> GetValidPictureTypes()
        {
            // Valid items: https://dev.twitter.com/rest/media/uploading-media
            List<string> list = new List<string>();
            list.Add("jpg");
            list.Add("png");
            list.Add("gif");
            list.Add("webp");

            return list;
        }

        /// <summary>
        /// Max size for any one image file
        /// Source: https://dev.twitter.com/rest/media/uploading-media
        /// </summary>
        public int MaxImageFileSize {  get { return MAX_IMAGE_FILE_SIZE; } }

        /// <summary>
        /// Max no of images per status
        /// Source: https://dev.twitter.com/rest/media/uploading-media
        /// </summary>
        public int MaxNoOfImages {  get { return MAX_IMAGES_PER_STATUS; } }

    }
}
