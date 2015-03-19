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

        /// <summary>
        /// Description: Sets the image of the button
        /// </summary>
        [Description("The image that will be displayed on the button"), Category("Calvin Custom Design")]
        public Image Image
        {
            get { return image; }
            set { image = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            if (image != null)
                pevent.Graphics.DrawImage(image, ClientRectangle);
        }
    }
}
