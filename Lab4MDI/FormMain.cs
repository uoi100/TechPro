using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                if (dialog.ShowDialog() == DialogResult.OK) ;
                {
                    FormChild child = new FormChild(dialog.Resolution);
                    child.MdiParent = this;
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
    }
}
