using SKZTweets.TwitterData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZTweets.Usercontrols.Interfaces
{
    public interface IThumbNailList
    {
        /// <summary>
        /// Replace image at given index with another image
        /// </summary>
        /// <param name="imageIndex"></param>
        /// <param name="newImage"></param>
        void InsertImage(int imageIndex, LocalImage newImage);

        /// <summary>
        /// Remove image at given index
        /// </summary>
        /// <param name="index"></param>
        void RemoveImage(int index);

        /// <summary>
        /// Called when a child is dealing with a drag enter event.
        /// Parent must be allowed to prevent dragging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        bool DragEnter(object sender, DragEventArgs e);

        /// <summary>
        /// Called when a child is dealing with a drag leave event.
        /// Parent must be allowed to clean up whatever it did during the DragEnter checking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DragLeave(object sender, EventArgs e);


        /// <summary>
        /// Called when a file has been dropped on an image contained by the parent.
        /// Parent needs to sort out how to add new things. Not up to the child, which IS a thing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="imageIndex"></param>
        void FileDropped(object sender, DragEventArgs e, int imageIndex);
    }
}
