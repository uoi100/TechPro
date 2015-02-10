using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4MDI
{
    public partial class FormChild : Form
    {
        private Image myImage;
        public FormChild()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Description: Constructor that takes a resolution, and creates an image that size of that resolution.
        /// </summary>
        /// <param name="size"></param>
        public FormChild(Size size)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
                g.Clear(Color.Blue);
            myImage = bmp;

            this.AutoScrollMinSize = size;
            InitializeComponent();
        }

        /// <summary>
        /// Description: Constructor that takes a filepath, and creates an image of that file from the path.
        /// </summary>
        /// <param name="file"></param>
        public FormChild(String file)
        {
            Bitmap bmp = new Bitmap(file);
            myImage = bmp;

            this.AutoScrollMinSize = myImage.Size;
            InitializeComponent();
        }

        /// <summary>
        /// Description: Set the image for the form to display.
        /// </summary>
        public Image Image
        {
            get { return myImage; }
            set { myImage = value; this.AutoScrollMinSize = myImage.Size; }
        }

        /// <summary>
        /// Description: Draws the image stored in the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(myImage, 0, 0);
        }


    }
}
