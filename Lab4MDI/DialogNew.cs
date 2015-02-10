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
    public partial class DialogNew : Form
    {
        Size size1 = new Size(640, 480);
        Size size2 = new Size(800, 600);
        Size size3 = new Size(1024, 768);
        public DialogNew(){
            InitializeComponent();
        }

        /// <summary>
        /// Description: Gets the resolution that was checked on the dialog box.
        /// </summary>
        public Size Resolution{
            get
            {
                if (radioButton1.Checked)
                    return size1;
                else if (radioButton2.Checked)
                    return size2;
                else
                    return size3;
            }
        }
    }
}
