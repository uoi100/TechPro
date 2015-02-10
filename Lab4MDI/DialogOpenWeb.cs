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
    public partial class DialogOpenWeb : Form
    {
        public DialogOpenWeb()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Description: Get the URL that the form contains
        /// </summary>
        public String Url
        {
            get { return textBox1.Text; }
        }
    }
}
