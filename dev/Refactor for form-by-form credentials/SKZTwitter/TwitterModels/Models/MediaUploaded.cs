using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterModels
{
    public class MediaUploaded
    {
        public ulong media_id { get; set; }
        public string media_id_string { get; set; }
        public int size { get; set; }
        public int expires_after_secs { get; set; }
        public MediaUploadedImage image { get; set; }
    }
}
