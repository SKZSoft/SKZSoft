using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SKZTweets.TwitterData.Models;
using SKZTweets.Usercontrols.Interfaces;
using SKZTweets.Models;

namespace SKZTweets.Usercontrols
{
    /// <summary>
    /// Control to display a tweet AND attached pictures
    /// </summary>
    public partial class ctlTweetTextAndPictures : ctlTweetText, IThumbNailList
    {
        private const int CONTROL_SPACING = 5;
        private const int THUMBNAIL_SPACING = 5;
        private List<LocalImage> m_images;
        private List<ctlLocalThumbnail> m_thumbnails;
        private const int BORDER_SIZE = 5;
        private ILocalImageContainer m_imageList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imageList"></param>
        public ctlTweetTextAndPictures(ILocalImageContainer imageList)
        {
            InitializeComponent();
            m_images = new List<LocalImage>();
            m_thumbnails = new List<ctlLocalThumbnail>();
            m_imageList = imageList;
        }


        private void ctlTweetTextAndPictures_DragEnter(object sender, DragEventArgs e)
        {
            DoDragEnter(sender, e);
        }

        private bool DoDragEnter(object sender, DragEventArgs e)
        {
            string errorMessage;
            if (DropDataValid(e.Data, out errorMessage))
            {
                System.Diagnostics.Debug.WriteLine("Drag allowed: copy");
                e.Effect = DragDropEffects.Copy;
                return true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Drag disallowed");
                
                // Show error - drop not allowed here
                lblDragDropError.Text = errorMessage;
                lblDragDropError.Top = 0;
                lblDragDropError.Left = 0;
                lblDragDropError.Width = this.ClientSize.Width;
                lblDragDropError.Height = this.ClientSize.Height;
                lblDragDropError.Visible = true;

                e.Effect = DragDropEffects.None;
                return false;
            }
        }



        private string[] GetDroppedFiles(IDataObject data)
        {
            string[] files = (string[])data.GetData(DataFormats.FileDrop);
            return files;
        }

        private LocalImageDragData GetLocalImageDragged(IDataObject data)
        {
            LocalImageDragData image = (LocalImageDragData)data.GetData(typeof(LocalImageDragData));
            return image;
        }

        private bool DropDataValid(IDataObject data, out string errorMessage)
        { 
            errorMessage = string.Empty;
            TwitterData.Models.TwitterConsts consts = new TwitterData.Models.TwitterConsts();

            LocalImageDragData localImage = GetLocalImageDragged(data);
            if (localImage != null)
            {
                if (m_images.Count == consts.MaxNoOfImages)
                {
                    if (localImage.ParentControl != this)
                    {
                        errorMessage = Strings.DragErrorTooManyPictures;
                        return false;
                    }
                }
                return true;
            }

            // are there files?
            if(!data.GetDataPresent(DataFormats.FileDrop))
            {
                errorMessage = Strings.DragErrorOnlyPictureFiles;
                return false;
            }
            string[] files = GetDroppedFiles(data);

            // are there too many files?
            if (files.Length + m_images.Count > consts.MaxNoOfImages)
            {
                errorMessage = Strings.DragErrorTooManyPictures;
                return false;
            }

            // Are the files all valid?
            List<string> validExtensions = consts.GetValidPictureTypes();
            bool isValidExt = true; //optimism
            foreach (string file in files)
            {
                // check off extension
                string ext = Path.GetExtension(file).ToLower();
                if(validExtensions.Contains(ext))
                {
                    // optimism unfounded
                    isValidExt = false;
                    break;
                }
            }

            if(!isValidExt)
            {
                errorMessage = Strings.DragErrorOnlyPictureFiles;
                return false;
            }

            return true;
        }

        private void ctlTweetTextAndPictures_DragDrop(object sender, DragEventArgs e)
        {
            DoDragDrop(sender, e, m_images.Count);
        }

        private void DoDragDrop(object sender, DragEventArgs e, int atIndex)
        { 
            // checked already on drag, but let's do it again anyway.
            string errorMessage;
            string[] files = GetDroppedFiles(e.Data);
            LocalImageDragData localImageDragged = GetLocalImageDragged(e.Data);

            if (!DropDataValid(e.Data, out errorMessage))
            {
                return;
            }

            // Add files
            if (files != null)
            {
                foreach (string file in files)
                {
                    AddFile(file, atIndex);
                }
            }

            if(localImageDragged != null)
            {
                // a local image has been dragged here from elsewhere
                localImageDragged.ParentControl.RemoveImage(localImageDragged.ParentIndex);
                m_images.Add(localImageDragged.LocalImage);
            }


            ShowImages();
        }


        public void AddFile(string pathname, int atIndex)
        {
            if(m_images.Count>=4)
            {
                throw new InvalidOperationException("Already have 4 images on this tweet");
            }

            LocalImage localImage = new LocalImage(pathname);
            localImage.IndexOnStatus = m_images.Count;
            localImage.Owner = this;
            m_images.Insert(atIndex, localImage);
        }

        private void M_RemoveClicked(object sender, EventArgs e)
        {
            ctlLocalThumbnail control = (ctlLocalThumbnail)sender;
            LocalImage localImage = control.LocalImage;
            m_images.Remove(localImage);
            ShowImages();
        }


        public void ShowImages()
        {
            // remove anything already here.
            foreach(ctlLocalThumbnail control in m_thumbnails)
            {
                control.RemoveClicked -= M_RemoveClicked;
                control.ParentTweetControl = null;
                pnlThumbnails.Controls.Remove(control);
                control.Dispose();
            }
            m_thumbnails = new List<ctlLocalThumbnail>();

            // add back in
            int left = 0;
            int maxHeight = 0;
            foreach(LocalImage localImage in m_images)
            {
                // create new thumbnail and place it on the panel
                ctlLocalThumbnail control = new ctlLocalThumbnail(m_imageList);
                control.RemoveClicked += M_RemoveClicked;
                control.BorderSize = BORDER_SIZE;
                control.LocalImage = localImage;
                control.ParentImageIndex = m_thumbnails.Count;
                control.ParentTweetControl = this;

                m_thumbnails.Add(control);
                pnlThumbnails.Controls.Add(control);

                control.Left = left;
                control.Width = control.RequiredWidth;
                control.Height = control.RequiredHeight;

                // move along to next position
                left += control.Width + THUMBNAIL_SPACING;

                // keep max height for later
                if (control.Height > maxHeight)
                {
                    maxHeight = control.Height;
                }
            }

            if(m_thumbnails.Count > 0)
            {
                pnlThumbnails.Height = maxHeight;
                pnlThumbnails.Visible = true;
                pnlThumbnails.Height = maxHeight;
                pnlThumbnails.Top = this.ClientSize.Height - maxHeight;
            }
            else
            {
                pnlThumbnails.Visible = false;
            }
            OnResizeRequired();
        }


        /// <summary>
        /// Resize self to show all text in textbox
        /// </summary>
        /// <returns></returns>
        public int ResizeToTextAndThumbnailHeight()
        {
            int newHeight = GetHeightRequired();
            this.Height = newHeight;

            return newHeight;
        }

        private int GetHeightRequired()
        {
            int newHeight = TextHeight + 10; // TODO: find out why "+10" is necessary and code around it.
            
            if (pnlThumbnails.Visible)
            {
                newHeight += pnlThumbnails.Height + CONTROL_SPACING;
            }
            return newHeight;
        }

        /// <summary>
        /// Raise when the status detects that it needs to be resized (eg when media is added/removed)
        /// </summary>
        public EventHandler<StatusResizeRequiredArgs> ResizeRequired;
        protected virtual void OnResizeRequired()
        {
            int newHeight = GetHeightRequired();
            StatusResizeRequiredArgs args = new StatusResizeRequiredArgs(newHeight);
            EventHandler<StatusResizeRequiredArgs> handler = ResizeRequired;
            if (handler != null)
            {
                handler(this, args);
            }
        }


        private void DoDragLeave(object sender, EventArgs e)
        {
            lblDragDropError.Visible = false;
        }

        private void ctlTweetTextAndPictures_DragLeave(object sender, EventArgs e)
        {
            DoDragLeave(sender, e);
        }

        void IThumbNailList.InsertImage(int imageIndex, LocalImage newImage)
        {
            m_images.Insert(imageIndex, newImage);
            ShowImages();
        }

        void IThumbNailList.RemoveImage(int index)
        {
            m_images.RemoveAt(index);
            ShowImages();
        }

        bool IThumbNailList.DragEnter(object sender, DragEventArgs e)
        {
            return DoDragEnter(sender, e);
        }

        void IThumbNailList.DragLeave(object sender, EventArgs e)
        {
            DoDragLeave(sender, e);
        }

        void IThumbNailList.FileDropped(object sender, DragEventArgs e, int imageIndex)
        {
            DoDragDrop(sender, e, imageIndex);
        }

        /// <summary>
        /// List of local images user has dragged onto this status
        /// </summary>
        public List<LocalImage> LocalImages {  get { return m_images; } }
    }
}
