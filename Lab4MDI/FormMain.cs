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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DialogNew dialog = new DialogNew())
            {
                if (dialog.ShowDialog() == DialogResult.OK) ;
                {
                    MessageBox.Show("Debug", "Testing", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
            }
        }
    }
}
