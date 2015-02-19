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
        private delegate void progressCallback(object sender, DoWorkEventArgs e);

        /// <summary>
        /// Description: Default constructor, initializes the images that will be used in the listview
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            ImageList imageList = new ImageList();

            imageList.Images.Add("file", Lab5GUIControls.Properties.Resources.file_icon);
            imageList.Images.Add("folder", Lab5GUIControls.Properties.Resources.directory_icon);
            listView1.LargeImageList = imageList;
            listView1.SmallImageList = imageList;
            closeBrowserToolStripMenuItem.Enabled = false;
            viewToolStripMenuItem.Enabled = false;
            text_Path.Visible = false;
            lbl_Path.Visible = false;
            progressBar1.Visible = false;
        }

        /// <summary>
        /// Description: Gets the files and folders at the root of the folder where this program is held at.
        /// </summary>
        private void rootList()
        {
            currentDirectory = new System.IO.DirectoryInfo(System.IO.Directory.GetDirectoryRoot(System.IO.Directory.GetCurrentDirectory()));

            DirectoryInfo[] folders = currentDirectory.GetDirectories();
            FileInfo[] files = currentDirectory.GetFiles();

            progressBar(folders.Length + files.Length);

            // Add all folders to the listview
            foreach (DirectoryInfo folder in folders)
            {
                var listViewItem = listView1.Items.Add(folder.Name);
                listViewItem.ImageKey = "folder";
            }

            // Add all files to the listview
            foreach(FileInfo file in files)
            {
                //listView1.Items.Add(files[i]);
                var listViewItem = listView1.Items.Add(file.Name);
                listViewItem.ImageKey = "file";
            }

            // Show the current path
            text_Path.Text = currentDirectory.FullName;
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

            progressBar(folders.Length + files.Length);

            foreach (DirectoryInfo folder in folders)
            {
                var listViewItem = listView1.Items.Add(folder.Name);
                listViewItem.ImageKey = "folder";
            }

            foreach (FileInfo file in files)
            {
                var listViewItem = listView1.Items.Add(file.Name);
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
                        Process.Start(currentDirectory.FullName + "/" + item.Text);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Description: Recursive function that updates the value of the progress bar until it is full.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_Progress_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.InvokeRequired)
            {
                progressCallback d = new progressCallback(worker_Progress_DoWork);
                this.Invoke(d, new object[] { sender, e });
            } else if(progressBar1.Value < progressBar1.Maximum)
            {
                if (progressBar1.Value + progressBar1.Step < progressBar1.Maximum)
                    progressBar1.Value += progressBar1.Step;
                else
                    progressBar1.Value = progressBar1.Maximum;
                worker_Progress.ReportProgress(progressBar1.Value);
                worker_Progress_DoWork(sender, e);
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
        /// Description: Go to the specified path directory, if the directory has a parent directory,
        /// then add a "Go up one level" option.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listView1.SelectedItems;

            try
            {
                foreach (ListViewItem item in items)
                    if (item.ImageKey == "...")         // Go up one level
                        changeDirectory(currentDirectory.Parent.FullName);
                    else if (item.ImageKey == "folder") // Enter folder
                        changeDirectory(currentDirectory.FullName + "/" + item.Text);
                
                text_Path.Text = currentDirectory.FullName;
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

        /// <summary>
        /// Description: When the file option items are clicked, if the user is opening a browser
        /// give a dialog box asking if the user wants to open the browser at the current directory,
        /// at the root directory, or cancel.
        /// When the user decides to close the browser, disable the listview and reset the
        /// view back to tiles.
        /// When the user clicks on the exit option, the application will close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileItems_Click(object sender, EventArgs e)
        {
            if (sender == openBrowserToolStripMenuItem)
            {
                listView1.Enabled = true;
                listView1.Visible = true;
                closeBrowserToolStripMenuItem.Enabled = true;
                openBrowserToolStripMenuItem.Enabled = false;
                viewToolStripMenuItem.Enabled = true;
                text_Path.Visible = true;
                lbl_Path.Visible = true;
                progressBar1.Visible = true;
                DialogOpenBrowser dialog = new DialogOpenBrowser();

                dialog.ShowDialog();

                if (dialog.DialogResult == DialogResult.OK)
                    changeDirectory(Directory.GetCurrentDirectory());
                else if (dialog.DialogResult == DialogResult.Yes)
                    rootList();

            }
            else if (sender == closeBrowserToolStripMenuItem)
            {
                listView1.Enabled = false;
                listView1.Visible = false;
                listView1.Clear();
                listView1.View = View.Tile;
                openBrowserToolStripMenuItem.Enabled = true;
                closeBrowserToolStripMenuItem.Enabled = false;
                viewToolStripMenuItem.Enabled = false;
                text_Path.Visible = false;
                lbl_Path.Visible = false;
                progressBar1.Visible = false;
            }
            else if (sender == exitToolStripMenuItem)
                this.Close();
        }

        /// <summary>
        /// Description: Sets the progress bar based on the number of files and folders the form has to load.
        /// </summary>
        /// <param name="num">Total number of files and folders to load</param>
        private void progressBar(int num)
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = 100;
            if (num > 0 && (progressBar1.Maximum / num) > 0)
                progressBar1.Step = progressBar1.Maximum / num;
            else
                progressBar1.Step = 1;
            progressBar1.Value = 0;
            worker_Progress.RunWorkerAsync();
        }

        // End of Class
    }
}
