using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Lab7CalvinTruongControlLibrary
{
    /// <summary>
    /// Description: A button which displays an image that can be clicked on
    /// </summary>
    public class PictureButton : System.Windows.Forms.Button
    {
        private Image image = null;
        private Image baseImage = null;
        private Image hoverImage = null;

        /// <summary>
        /// Description: Sets the image of the button
        /// </summary>
        [Description("The image that will be displayed on the button"), Category("Calvin Custom Design")]
        public Image Image
        {
            get { return baseImage; }
            set { baseImage = value; Invalidate(); image = baseImage; }
        }

        /// <summary>
        /// Description: Sets the image of the button that is used in the event the mouse is over the button
        /// </summary>
        [Description("The image that will be displaye when the mouse hovers over the button"), Category("Calvin Custom Design")]
        public Image HoverImage
        {
            get { return hoverImage; }
            set { hoverImage = value; Invalidate(); }
        }

        /// <summary>
        /// Description: When the mouse enters the button, change the button image to its hover image.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);
            if (hoverImage == null) return;
            image = hoverImage;
        }

        /// <summary>
        /// Description: When the mouse leaves the button, change the button back to its base image.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(System.EventArgs e)
        {
            base.OnMouseLeave(e);
            image = baseImage;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            if (image != null)
                pevent.Graphics.DrawImage(image, ClientRectangle);
        }
    }
}
