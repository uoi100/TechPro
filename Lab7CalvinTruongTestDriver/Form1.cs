using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7CalvinTruongTestDriver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Description: When the event is triggered, close the program.
        /// </summary>
        /// <param name="sender">The button that was clicked to trigger the event</param>
        /// <param name="e"></param>
        private void colorButton1_CalvinEvent(object sender, EventArgs e)
        {
            Close();
        }
    }
}
