using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using SKZTweets.TwitterData.Models;
using System.Diagnostics;
using SKZTweets.Models;
using SKZTweets.Usercontrols.Interfaces;

namespace SKZTweets.Usercontrols
{
    public partial class ctlLocalThumbnail : UserControl
    {
        private int m_borderSize;
        private LocalImage m_localImage;
        private Point m_mouseDownPosition;
        private bool m_mouseWasDown;           // Point  is not a nullable type so we have to also use a bool to indicate if it's got data.
        private bool m_dragging;
        private ILocalImageContainer m_imageList;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imageList"></param>
        public ctlLocalThumbnail(ILocalImageContainer imageList)
        {
            InitializeComponent();
            m_imageList = imageList;
        }

        /// <summary>
        /// When the "X" is clicked to remove an image
        /// </summary>
        public EventHandler<EventArgs> RemoveClicked;
        protected virtual void OnRemoveClicked()
        {
            EventHandler<EventArgs> handler = RemoveClicked;
            if (handler != null)
            {
                EventArgs e = new EventArgs();
                handler(this, e);
            }
        }

        /// <summary>
        /// "Close" label clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_Click(object sender, EventArgs e)
        {
            OnRemoveClicked();
        }

        /// <summary>
        /// The file to show as a thumbnail (must be a local file)
        /// </summary>
        public LocalImage LocalImage
        {
            get
            {
                return m_localImage;
            }
            set
            {
                m_localImage = value;
                picImage.Image = LocalImage.Thumbnail;
            }
        }

        /// <summary>
        /// Size of the border to display around thumbnails
        /// </summary>
        public int BorderSize
        {
            get { return m_borderSize; }
            set
            {
                m_borderSize = value;
                picImage.Top = value;
                picImage.Left = value;
            }
        }

        /// <summary>
        /// The width of this control which is required to display the thumbnail and border completely.
        /// </summary>
        public int RequiredWidth { get { return picImage.Width + (BorderSize * 2); } }

        /// <summary>
        /// The height of this control which is required to display the thumbnail and border completely.
        /// </summary>
        public int RequiredHeight { get { return picImage.Height + (BorderSize * 2); } }

        /// <summary>
        /// Picture double clicked. Launch it for viewing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picImage_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(LocalImage.Path);
        }


        #region DragStart
        // This has to be handrolled. Yes, even in 2017.
        // Catch and process mouse activity to see if users is dragging or clicking.
        private void picImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_mouseDownPosition = e.Location;
                m_mouseWasDown = true;
            }
        }

        private void picImage_MouseUp(object sender, MouseEventArgs e)
        {
            m_mouseWasDown = false;

            if (m_dragging)
            {
                // we WERE dragging. Mouse has gone up, and it's over this same control.
                // Mark operation as aborted
                m_dragging = false;
                return;
            }

            // Mouse went down, and now it went up.
            // Click behaviour. Launch picture viewer.
            Process.Start(LocalImage.Path);
        }

        /// <summary>
        /// Work out if mouse has moved far enough to begin a drag drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picImage_MouseMove(object sender, MouseEventArgs e)
        {
            // never if button wasn't or isn't pressed
            if (!m_mouseWasDown || e.Button != MouseButtons.Left)
            {
                return;
            }

            // get distance moved
            int distX = Math.Abs(e.Location.X - m_mouseDownPosition.X);
            int distY = Math.Abs(e.Location.Y - m_mouseDownPosition.Y);

            if (distX >= System.Windows.SystemParameters.MinimumHorizontalDragDistance || distY >= System.Windows.SystemParameters.MinimumVerticalDragDistance)
            {
                // the user has begun a drag operation
                LocalImageDragData dragData = new LocalImageDragData(m_localImage, ParentTweetControl, ParentImageIndex);
                DoDragDrop(dragData, DragDropEffects.All);
            }
        }
        #endregion


        /// <summary>
        /// The index of THIS control WITHIN the parent control
        /// </summary>
        public int ParentImageIndex { get; set; }


        /// <summary>
        /// The tweet display control in which this image sits.
        /// </summary>
        public IThumbNailList ParentTweetControl { get; set; }


        /// <summary>
        /// Drag enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlLocalThumbnail_DragEnter(object sender, DragEventArgs e)
        {
            // give parent first chance to disallow
            if(!ParentTweetControl.DragEnter(sender, e))
            {
                // parent disallows any drop here.
                return;
            }


            if (e.Data.GetDataPresent(typeof(LocalImageDragData)))
            {
                LocalImageDragData data = (LocalImageDragData)e.Data.GetData(typeof(LocalImageDragData));
                if (data.LocalImage == m_localImage)
                {
                    // this is me. Do nothing
                    return;
                }
            }
            e.Effect = DragDropEffects.Move;
            this.BackColor = Color.Black;
        }

        /// <summary>
        /// Drag leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlLocalThumbnail_DragLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            ParentTweetControl.DragLeave(sender, e);
        }


        /// <summary>
        /// Drag drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlLocalThumbnail_DragDrop(object sender, DragEventArgs e)
        {
            // reset background colour
            this.BackColor = Color.White;

            if (e.Data.GetDataPresent(typeof(LocalImageDragData)))
            {
                // get local image file
                LocalImageDragData droppedData = (LocalImageDragData)e.Data.GetData(typeof(LocalImageDragData));

                // THAT image has been dropped on THIS instance.
                // Raise an event to parent to let it know they need to be swapped around.
                LocalImageDragData thisData = new LocalImageDragData(m_localImage, ParentTweetControl, ParentImageIndex);

                m_imageList.SwapImages(droppedData, thisData);
                return;
            }

            ParentTweetControl.FileDropped(sender, e, ParentImageIndex);
        }
    }
}
