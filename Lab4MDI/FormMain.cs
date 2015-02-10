using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

/**
 * @Author      Calvin Truong
 * @Course      Comp3951
 * @StudentID   A00812171
 * @Name        Lab4MDI
 * @Purpose     Create a MDI Application that has child containers, can load images from file and web.
 **/
namespace Lab4MDI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Description: When the new accelerator is clicked, call a dialog to specify the size of the new childform.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DialogNew dialog = new DialogNew())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FormChild child = new FormChild(dialog.Resolution);
                    child.MdiParent = this;
                    child.FormClosing += new System.Windows.Forms.FormClosingEventHandler(childClosing);
                    saveToolStripMenuItem.Enabled = true;
                    saveAsToolStripMenuItem.Enabled = true;
                    child.Show();
                }
            }
        }

        /// <summary>
        /// Description: Cascades the child forms within this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        /// <summary>
        /// Description: Tile the child forms horizontally      
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileHorizontallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        /// <summary>
        /// Description: Tile the child forms vertically
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        /// <summary>
        /// Description: Open a file from the file explorer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (*.jpg, *.jpeg, *.bmp, *.gif)|*.jpg; *.jpeg; *.bmp; *.gif|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FormChild child = new FormChild(openFileDialog1.FileName);
                child.Url = openFileDialog1.FileName;
                child.MdiParent = this;
                child.FormClosing += new System.Windows.Forms.FormClosingEventHandler(childClosing);
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                child.Show();
            }
        }

        /// <summary>
        /// Description: Open a file from the web
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFromWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogOpenWeb dialog = new DialogOpenWeb();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    WebRequest request = WebRequest.Create(dialog.Url);
                    WebResponse response = request.GetResponse();

                    Image image = Image.FromStream(response.GetResponseStream());
                    FormChild child = new FormChild();
                    child.Image = image;
                    child.MdiParent = this;
                    child.FormClosing += new System.Windows.Forms.FormClosingEventHandler(childClosing);
                    saveToolStripMenuItem.Enabled = true;
                    saveAsToolStripMenuItem.Enabled = true;
                    child.Show();
                }
                catch (Exception exception)
                {
                    MessageBox.Show( exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Description: Save the current image of the currently focused child form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChild child = (FormChild) this.ActiveMdiChild;

            saveFileDialog1.Filter = "JPeg Image| *.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";

            if (child.Url == String.Empty)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    child.save(saveFileDialog1.FileName);
                }
            }
            else
            {
                child.save();
            }
        }

        /// <summary>
        /// Description: Save the image to a specified location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChild child = (FormChild)this.ActiveMdiChild;
            saveFileDialog1.Filter = "JPeg Image| *.jpg; *.jpeg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                child.save(saveFileDialog1.FileName);
            
        }

        /// <summary>
        /// Description: If the child is closing, create a dialog that will handle how the user wants the child to close.
        /// The dialog will allow the form to save, save as, close, or cancel closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void childClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FormChild child = (FormChild)sender;
            DialogClosing dialog = new DialogClosing();
            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:   // Save
                    saveToolStripMenuItem_Click(sender, e);
                    break;
                case DialogResult.Yes:  // Save-As
                    saveAsToolStripMenuItem_Click(sender, e);
                    break;
                case DialogResult.No: // No
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }

            // Checks if there are still child forms, if there is not disable the toolstrips
            if (this.MdiChildren.Length - 1 <= 0)
            {
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
            }
        }

    }
}
