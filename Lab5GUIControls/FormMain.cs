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

            FileInfo[] files = currentDirectory.GetFiles();

            //retrieve all image files
            foreach(FileInfo file in files)
            {
                //listView1.Items.Add(files[i]);
                var listViewItem = listView1.Items.Add(file.FullName);
                listViewItem.ImageKey = "file";
            }

            DirectoryInfo[] folders = currentDirectory.GetDirectories();

            foreach (DirectoryInfo folder in folders)
            {
                var listViewItem = listView1.Items.Add(folder.FullName);
                listViewItem.ImageKey = "folder";
            }

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
            FileInfo[] files = currentDirectory.GetFiles();
            DirectoryInfo[] folders = currentDirectory.GetDirectories();

            if (currentDirectory.Parent != null)
            {
                ListViewItem item = listView1.Items.Add("Go Up One Level");
                item.ImageKey = "...";
            }

            foreach (FileInfo file in files)
            {
                var listViewItem = listView1.Items.Add(file.FullName);
                listViewItem.ImageKey = "file";
            }

            foreach (DirectoryInfo folder in folders)
            {
                var listViewItem = listView1.Items.Add(folder.FullName);
                listViewItem.ImageKey = "folder";
            }

        }

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

        private void worker_Progress_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                worker_Progress.ReportProgress(i);
            }
        }

        private void worker_Progress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

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

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }

        private void tileDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
        }

        private void smallIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }

        private void largeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }

        // End of Class
    }
}
