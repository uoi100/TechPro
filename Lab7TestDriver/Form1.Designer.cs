namespace Lab7TestDriver
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.calvinButton1 = new CalvinCustom.CalvinButton();
            this.pictureButton1 = new CalvinCustom.PictureButton();
            this.SuspendLayout();
            // 
            // calvinButton1
            // 
            this.calvinButton1.ColorA = System.Drawing.Color.Red;
            this.calvinButton1.ColorA_Alpha = 100;
            this.calvinButton1.ColorB = System.Drawing.Color.Blue;
            this.calvinButton1.ColorB_Alpha = 100;
            this.calvinButton1.Location = new System.Drawing.Point(27, 32);
            this.calvinButton1.Name = "calvinButton1";
            this.calvinButton1.Size = new System.Drawing.Size(207, 79);
            this.calvinButton1.TabIndex = 0;
            this.calvinButton1.Text = "Click to Exit";
            this.calvinButton1.UseVisualStyleBackColor = true;
            this.calvinButton1.CalvinEvent += new System.EventHandler(this.Exit);
            // 
            // pictureButton1
            // 
            this.pictureButton1.HoverImage = global::Lab7TestDriver.Properties.Resources.Exit_Hover;
            this.pictureButton1.Image = global::Lab7TestDriver.Properties.Resources.Exit;
            this.pictureButton1.Location = new System.Drawing.Point(27, 127);
            this.pictureButton1.Name = "pictureButton1";
            this.pictureButton1.Size = new System.Drawing.Size(207, 108);
            this.pictureButton1.TabIndex = 1;
            this.pictureButton1.Text = "pictureButton1";
            this.pictureButton1.UseVisualStyleBackColor = true;
            this.pictureButton1.Click += new System.EventHandler(this.Exit);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pictureButton1);
            this.Controls.Add(this.calvinButton1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private CalvinCustom.CalvinButton calvinButton1;
        private CalvinCustom.PictureButton pictureButton1;
    }
}

