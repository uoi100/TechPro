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
        private String url;
        public FormChild()
        {
            url = String.Empty;
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
            url = String.Empty;
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

            url = file;
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
        /// Description: Url of the file that the form contains.
        /// </summary>
        public String Url
        {
            get { return url; }
            set { url = value; }
        }

        /// <summary>
        /// Description: Save image at the url location stored on this form
        /// </summary>
        public void save()
        {
            try
            {
                Bitmap bmp = new Bitmap(myImage);
                myImage.Dispose();

                myImage = bmp;
                myImage.Save(url);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Description: Save image at the specified url location
        /// </summary>
        /// <param name="url"></param>
        public void save(string url)
        {
            try
            {
                Bitmap bmp = new Bitmap(myImage);
                myImage.Dispose();

                myImage = bmp;
                myImage.Save(url);
                this.url = url;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Description: Draws the image stored in the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repaint(object sender, PaintEventArgs e)
        {
            pictureBox.Image = myImage;
        }

    }
}
