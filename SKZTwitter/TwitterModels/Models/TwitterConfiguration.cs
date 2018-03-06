using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets.TwitterModels
{
    public class TwitterConfiguration
    {
        public int characters_reserved_per_media { get; set; }
        public int dm_text_character_limit { get; set; }
        public int max_media_per_upload { get; set; }
        public int photo_size_limit { get; set; }
        public Photo_Sizes photo_sizes { get; set; }
        public int short_url_length { get; set; }
        public int short_url_length_https { get; set; }
        public string[] non_username_paths { get; set; }
    }

}
