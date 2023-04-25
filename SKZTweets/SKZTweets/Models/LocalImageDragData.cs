using SKZSoft.Twitter.TwitterData.Models;
using SKZSoft.SKZTweets.Usercontrols;
using SKZSoft.SKZTweets.Usercontrols.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.SKZTweets.Models
{
    public class LocalImageDragData
    {
        public LocalImage LocalImage { get; set; }
        public IThumbNailList ParentControl { get; set; }
        public int ParentIndex { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localImage"></param>
        /// <param name="parentControl"></param>
        /// <param name="parentIndex"></param>
        public LocalImageDragData(LocalImage localImage, IThumbNailList parentControl, int parentIndex)
        {
            LocalImage = localImage;
            ParentControl = parentControl;
            ParentIndex = parentIndex;
        }


        /// <summary>
        /// Remove from the parent control
        /// </summary>
        public void RemovedFromParent()
        {
            ParentControl.RemoveImage(ParentIndex);
        }
    }
}
