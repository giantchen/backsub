namespace Viewer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
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
          this.mainMenu1 = new System.Windows.Forms.MainMenu();
          this.picMain = new System.Windows.Forms.PictureBox();
          this.btRedraw = new System.Windows.Forms.Button();
          this.SuspendLayout();
          // 
          // picMain
          // 
          this.picMain.Location = new System.Drawing.Point(3, 3);
          this.picMain.Name = "picMain";
          this.picMain.Size = new System.Drawing.Size(234, 176);
          this.picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
          // 
          // btRedraw
          // 
          this.btRedraw.Location = new System.Drawing.Point(3, 245);
          this.btRedraw.Name = "btRedraw";
          this.btRedraw.Size = new System.Drawing.Size(72, 20);
          this.btRedraw.TabIndex = 1;
          this.btRedraw.Text = "Redraw";
          this.btRedraw.Click += new System.EventHandler(this.btRedraw_Click);
          // 
          // MainForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
          this.AutoScroll = true;
          this.ClientSize = new System.Drawing.Size(240, 268);
          this.Controls.Add(this.btRedraw);
          this.Controls.Add(this.picMain);
          this.KeyPreview = true;
          this.Menu = this.mainMenu1;
          this.Name = "MainForm";
          this.Text = "²é¿´";
          this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
          this.ResumeLayout(false);

        }

        #endregion

      private System.Windows.Forms.PictureBox picMain;
      private System.Windows.Forms.Button btRedraw;
    }
}

