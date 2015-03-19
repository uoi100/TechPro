using System;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Controls;
using System.ComponentModel;

namespace Lab7CalvinTruongControlLibrary
{
    /// <summary>
    /// Description: Button that has a set of two colors painted into a pattern
    /// </summary>
    public class ColorButton : System.Windows.Forms.Button
    {
        private Color colorA_ = Color.Empty;
        private Color colorB_ = Color.Empty;
        private int colorA_Alpha_ = 100;
        private int colorB_Alpha_ = 100;
        private event EventHandler calvinEvent;

        /// <summary>
        /// Description: Get and Set method for the primary color
        /// </summary>
        [Description("Primary Color displayed on the left-side of the button"), Category("Calvin Custom Design")]
        public Color ColorA
        {
            get { return colorA_; }
            set { colorA_ = value; Invalidate(); }
        }

        /// <summary>
        /// Description: Get and Set method for the secondary color
        /// </summary>
        [Description("Secondary Color displayed on the right-side of the button"), Category("Calvin Custom Design")]
        public Color ColorB
        {
            get { return colorB_; }
            set { colorB_ = value; Invalidate(); }
        }

        /// <summary>
        /// Description: Get and Set method for the primary color transparency
        /// </summary>
        [Description("Transparency of the Primary Color"), Category("Calvin Custom Design")]
        public int ColorA_Alpha
        {
            get { return colorA_Alpha_; }
            set { colorA_Alpha_ = value; Invalidate(); }
        }

        /// <summary>
        /// Description: Get and Set method for the secondary color transparency
        /// </summary>
        [Description("Transparency of the Secondary Color"), Category("Calvin Custom Design")]
        public int ColorB_Alpha
        {
            get { return colorB_Alpha_; }
            set { colorB_Alpha_ = value; Invalidate(); }
        }

        /// <summary>
        /// Description: a custom mouse event that occurs when the button is clicked with the left mouse button.
        /// </summary>
        [Description("a custom mouse event that occurs when the button is clicked with the left mouse button."), Category("Calvin Custom Design")]
        public event EventHandler CalvinEvent
        {
            add
            {
                base.Click += value;
            }
            remove
            {
                base.Click -= value;
            }
        }


        /// <summary>
        /// Description: The text that will be displayed on the button
        /// </summary>
        [Description("Text that will be displayed on the button"), Category("Calvin Custom Design")]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; Invalidate(); }
        }
       

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            // Create two semi-transparent colors
            Color c1 = Color.FromArgb(colorA_Alpha_, colorA_);
            Color c2 = Color.FromArgb(colorB_Alpha_, colorB_);
            using (Brush b = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, c1, c2, 10))
                pevent.Graphics.FillRectangle(b, ClientRectangle);
            
        }
    }
}
