using SKZTweets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets.Usercontrols.Interfaces
{
    public interface ILocalImageContainer
    {

        void SwapImages(LocalImageDragData draggedImage, LocalImageDragData droppedOn);
    }
}
