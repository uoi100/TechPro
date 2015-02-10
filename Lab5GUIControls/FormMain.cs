using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Lab5GUIControls
{
    public partial class FormMain : Form
    {
        private System.IO.DirectoryInfo currentDirectory;

        /// <summary>
        /// Description: Default constructor, initializes the images that will be used in the listview
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            ImageList imageList = new ImageList();

            imageList.Images.Add("file", Lab5GUIControls.Properties.Resources.file);
            imageList.Images.Add("folder", Lab5GUIControls.Properties.Resources.folder);
            listView1.LargeImageList = imageList;
            listView1.SmallImageList = imageList;

            testList();
        }

        /// <summary>
        /// Description: Gets the files and folders at the root of the folder where this program is held at.
        /// </summary>
        private void testList()
        {
            currentDirectory = new System.IO.DirectoryInfo(System.IO.Directory.GetDirectoryRoot(System.IO.Directory.GetCurrentDirectory()));

            DirectoryInfo[] folders = currentDirectory.GetDirectories();
            FileInfo[] files = currentDirectory.GetFiles();


            // Add all folders to the listview
            foreach (DirectoryInfo folder in folders)
            {
                var listViewItem = listView1.Items.Add(folder.FullName);
                listViewItem.ImageKey = "folder";
            }

            // Add all files to the listview
            foreach(FileInfo file in files)
            {
                //listView1.Items.Add(files[i]);
                var listViewItem = listView1.Items.Add(file.FullName);
                listViewItem.ImageKey = "file";
            }

            // Show the current path
            text_Path.Text = "Path: " + currentDirectory.FullName;
        }

        /// <summary>
        /// Description: Change the current directory to the specified path
        /// </summary>
        /// <param name="path"></param>
        private void changeDirectory(string path)
        {
            listView1.Clear();

            currentDirectory = new System.IO.DirectoryInfo(path);
            DirectoryInfo[] folders = currentDirectory.GetDirectories();
            FileInfo[] files = currentDirectory.GetFiles();

            // If there is a parent folder, add a Go Up One Level option
            if (currentDirectory.Parent != null)
            {
                ListViewItem item = listView1.Items.Add("Go Up One Level");
                item.ImageKey = "...";
            }

            foreach (DirectoryInfo folder in folders)
            {
                var listViewItem = listView1.Items.Add(folder.FullName);
                listViewItem.ImageKey = "folder";
            }

            foreach (FileInfo file in files)
            {
                var listViewItem = listView1.Items.Add(file.FullName);
                listViewItem.ImageKey = "file";
            }
        }

        /// <summary>
        /// Description: If an item is double clicked, check if it is a file, if it is open the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listView1.SelectedItems;

            try
            {
                foreach (ListViewItem item in items)
                {
                    if( item.ImageKey == "file" )
                        Process.Start(item.Text);
                }

                text_Path.Text = "Path: " + currentDirectory.FullName;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Description: Worker in the background that is updating the progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_Progress_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                worker_Progress.ReportProgress(i);
            }
        }

        /// <summary>
        /// Description: Update the value of the progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_Progress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Description: When a folder item is clicked, go into the directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listView1.SelectedItems;

            try
            {
                foreach (ListViewItem item in items)
                {
                    if (item.ImageKey == "...")
                    {

                        if (currentDirectory.Parent != null)
                        {
                            progressBar1.Style = ProgressBarStyle.Continuous;
                            progressBar1.Maximum = 100;
                            progressBar1.Value = 0;
                            progressBar1.Step = 1;
                            worker_Progress.RunWorkerAsync();
                            changeDirectory(currentDirectory.Parent.FullName);
                        }
                    }
                    else if (item.ImageKey == "folder")
                    {
                        progressBar1.Style = ProgressBarStyle.Continuous;
                        progressBar1.Maximum = 100;
                        progressBar1.Value = 0;
                        progressBar1.Step = 1;
                        worker_Progress.RunWorkerAsync();
                        changeDirectory(item.Text);
                    }
                }

                text_Path.Text = "Path: " + currentDirectory.FullName;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Description: When a view menu option is selected, change the listview's view to the respective option that was selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewItems_Click(object sender, EventArgs e)
        {
            if(sender == listToolStripMenuItem)
                listView1.View = View.List;
            else if(sender == tileDefaultToolStripMenuItem)
                listView1.View = View.Tile;
            else if(sender == smallIconsToolStripMenuItem)
                listView1.View = View.SmallIcon;
            else if(sender == largeIconsToolStripMenuItem)
                listView1.View = View.LargeIcon;
        }

        private void fileItems_Click(object sender, EventArgs e)
        {
            if (sender == openBrowserToolStripMenuItem)
            {
                listView1.Clear();
                listView1.Enabled = true;
                listView1.Visible = true;
                testList();
            }
            else if (sender == closeBrowserToolStripMenuItem)
            {
                listView1.Enabled = false;
                listView1.Visible = false;
            }
            else if (sender == exitToolStripMenuItem)
                this.Close();
        }

        // End of Class
    }
}
