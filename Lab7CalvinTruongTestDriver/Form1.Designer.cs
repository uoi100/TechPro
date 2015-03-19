namespace Lab7CalvinTruongTestDriver
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
            this.colorButton1 = new Lab7CalvinTruongControlLibrary.ColorButton();
            this.pictureButton1 = new Lab7CalvinTruongControlLibrary.PictureButton();
            this.SuspendLayout();
            // 
            // colorButton1
            // 
            this.colorButton1.ColorA = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorButton1.ColorA_Alpha = 100;
            this.colorButton1.ColorB = System.Drawing.Color.Yellow;
            this.colorButton1.ColorB_Alpha = 100;
            this.colorButton1.Location = new System.Drawing.Point(28, 12);
            this.colorButton1.Name = "colorButton1";
            this.colorButton1.Size = new System.Drawing.Size(188, 92);
            this.colorButton1.TabIndex = 0;
            this.colorButton1.Text = "Click to Exit";
            this.colorButton1.UseVisualStyleBackColor = true;
            this.colorButton1.CalvinEvent += new System.EventHandler(this.colorButton1_CalvinEvent);
            // 
            // pictureButton1
            // 
            this.pictureButton1.HoverImage = global::Lab7CalvinTruongTestDriver.Properties.Resources.Exit_Hover;
            this.pictureButton1.Image = global::Lab7CalvinTruongTestDriver.Properties.Resources.Exit;
            this.pictureButton1.Location = new System.Drawing.Point(28, 130);
            this.pictureButton1.Name = "pictureButton1";
            this.pictureButton1.Size = new System.Drawing.Size(188, 81);
            this.pictureButton1.TabIndex = 1;
            this.pictureButton1.Text = "pictureButton1";
            this.pictureButton1.UseVisualStyleBackColor = true;
            this.pictureButton1.Click += new System.EventHandler(this.colorButton1_CalvinEvent);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pictureButton1);
            this.Controls.Add(this.colorButton1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Lab7CalvinTruongControlLibrary.ColorButton colorButton1;
        private Lab7CalvinTruongControlLibrary.PictureButton pictureButton1;
    }
}

