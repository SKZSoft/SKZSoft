using SKZSoft.Twitter.TwitterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.SKZTweets.Models;

namespace SKZSoft.SKZTweets.Usercontrols.Interfaces
{
    public interface ILocalImageContainer
    {

        void SwapImages(LocalImageDragData draggedImage, LocalImageDragData droppedOn);
    }
}
