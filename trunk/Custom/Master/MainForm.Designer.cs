namespace Master
{
    partial class MainForm
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
          this.components = new System.ComponentModel.Container();
          this.picMain = new System.Windows.Forms.PictureBox();
          this.btStart = new System.Windows.Forms.Button();
          this.btStop = new System.Windows.Forms.Button();
          this.statusStrip = new System.Windows.Forms.StatusStrip();
          this.timerMove = new System.Windows.Forms.Timer(this.components);
          this.btPda1 = new System.Windows.Forms.Button();
          ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
          this.SuspendLayout();
          // 
          // picMain
          // 
          this.picMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.picMain.BackColor = System.Drawing.Color.White;
          this.picMain.Location = new System.Drawing.Point(93, 12);
          this.picMain.Name = "picMain";
          this.picMain.Size = new System.Drawing.Size(768, 576);
          this.picMain.TabIndex = 0;
          this.picMain.TabStop = false;
          // 
          // btStart
          // 
          this.btStart.Location = new System.Drawing.Point(12, 12);
          this.btStart.Name = "btStart";
          this.btStart.Size = new System.Drawing.Size(75, 23);
          this.btStart.TabIndex = 1;
          this.btStart.Text = "Start";
          this.btStart.UseVisualStyleBackColor = true;
          this.btStart.Click += new System.EventHandler(this.btStart_Click);
          // 
          // btStop
          // 
          this.btStop.Location = new System.Drawing.Point(12, 41);
          this.btStop.Name = "btStop";
          this.btStop.Size = new System.Drawing.Size(75, 23);
          this.btStop.TabIndex = 2;
          this.btStop.Text = "Stop";
          this.btStop.UseVisualStyleBackColor = true;
          this.btStop.Click += new System.EventHandler(this.btStop_Click);
          // 
          // statusStrip
          // 
          this.statusStrip.Location = new System.Drawing.Point(0, 591);
          this.statusStrip.Name = "statusStrip";
          this.statusStrip.Size = new System.Drawing.Size(873, 22);
          this.statusStrip.TabIndex = 3;
          this.statusStrip.Text = "statusStrip";
          // 
          // timerMove
          // 
          this.timerMove.Interval = 1000;
          this.timerMove.Tick += new System.EventHandler(this.timerMove_Tick);
          // 
          // btPda1
          // 
          this.btPda1.Location = new System.Drawing.Point(12, 80);
          this.btPda1.Name = "btPda1";
          this.btPda1.Size = new System.Drawing.Size(75, 23);
          this.btPda1.TabIndex = 4;
          this.btPda1.Text = "PDA 1";
          this.btPda1.UseVisualStyleBackColor = true;
          this.btPda1.Click += new System.EventHandler(this.btPda1_Click);
          // 
          // MainForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(873, 613);
          this.Controls.Add(this.btPda1);
          this.Controls.Add(this.statusStrip);
          this.Controls.Add(this.btStop);
          this.Controls.Add(this.btStart);
          this.Controls.Add(this.picMain);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.Name = "MainForm";
          this.Text = "º‡ ”";
          this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
          this.Move += new System.EventHandler(this.MainForm_Move);
          ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

      private System.Windows.Forms.PictureBox picMain;
      private System.Windows.Forms.Button btStart;
      private System.Windows.Forms.Button btStop;
      private System.Windows.Forms.StatusStrip statusStrip;
      private System.Windows.Forms.Timer timerMove;
      private System.Windows.Forms.Button btPda1;
    }
}

