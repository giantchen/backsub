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
          System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("出勤", System.Windows.Forms.HorizontalAlignment.Left);
          System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("未出勤", System.Windows.Forms.HorizontalAlignment.Left);
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
          this.picMain = new System.Windows.Forms.PictureBox();
          this.btStart = new System.Windows.Forms.Button();
          this.btStop = new System.Windows.Forms.Button();
          this.statusStrip = new System.Windows.Forms.StatusStrip();
          this.timerMove = new System.Windows.Forms.Timer(this.components);
          this.btPda1 = new System.Windows.Forms.Button();
          this.btPda2 = new System.Windows.Forms.Button();
          this.txLocalIp = new System.Windows.Forms.TextBox();
          this.txPda1Ip = new System.Windows.Forms.TextBox();
          this.txPda2Ip = new System.Windows.Forms.TextBox();
          this.lvPda = new System.Windows.Forms.ListView();
          this.chPda = new System.Windows.Forms.ColumnHeader();
          this.imageList1 = new System.Windows.Forms.ImageList(this.components);
          this.timerUpdate = new System.Windows.Forms.Timer(this.components);
          this.btGrp1 = new System.Windows.Forms.Button();
          ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
          this.SuspendLayout();
          // 
          // picMain
          // 
          this.picMain.BackColor = System.Drawing.Color.White;
          this.picMain.Location = new System.Drawing.Point(93, 12);
          this.picMain.Name = "picMain";
          this.picMain.Size = new System.Drawing.Size(480, 360);
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
          this.statusStrip.Location = new System.Drawing.Point(0, 524);
          this.statusStrip.Name = "statusStrip";
          this.statusStrip.Size = new System.Drawing.Size(584, 22);
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
          this.btPda1.Location = new System.Drawing.Point(12, 198);
          this.btPda1.Name = "btPda1";
          this.btPda1.Size = new System.Drawing.Size(75, 23);
          this.btPda1.TabIndex = 4;
          this.btPda1.Text = "PDA 1";
          this.btPda1.UseVisualStyleBackColor = true;
          this.btPda1.Click += new System.EventHandler(this.btPda1_Click);
          // 
          // btPda2
          // 
          this.btPda2.Location = new System.Drawing.Point(12, 254);
          this.btPda2.Name = "btPda2";
          this.btPda2.Size = new System.Drawing.Size(75, 23);
          this.btPda2.TabIndex = 5;
          this.btPda2.Text = "PDA 2";
          this.btPda2.UseVisualStyleBackColor = true;
          this.btPda2.Click += new System.EventHandler(this.btPda2_Click);
          // 
          // txLocalIp
          // 
          this.txLocalIp.Location = new System.Drawing.Point(12, 70);
          this.txLocalIp.Multiline = true;
          this.txLocalIp.Name = "txLocalIp";
          this.txLocalIp.Size = new System.Drawing.Size(75, 66);
          this.txLocalIp.TabIndex = 6;
          // 
          // txPda1Ip
          // 
          this.txPda1Ip.Location = new System.Drawing.Point(12, 171);
          this.txPda1Ip.Name = "txPda1Ip";
          this.txPda1Ip.Size = new System.Drawing.Size(75, 21);
          this.txPda1Ip.TabIndex = 7;
          this.txPda1Ip.Text = "192.168.1.3";
          // 
          // txPda2Ip
          // 
          this.txPda2Ip.Location = new System.Drawing.Point(12, 227);
          this.txPda2Ip.Name = "txPda2Ip";
          this.txPda2Ip.Size = new System.Drawing.Size(75, 21);
          this.txPda2Ip.TabIndex = 8;
          this.txPda2Ip.Text = "192.168.1.4";
          // 
          // lvPda
          // 
          this.lvPda.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPda});
          listViewGroup3.Header = "出勤";
          listViewGroup3.Name = "Online";
          listViewGroup4.Header = "未出勤";
          listViewGroup4.Name = "Offline";
          this.lvPda.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
          this.lvPda.Location = new System.Drawing.Point(93, 378);
          this.lvPda.Name = "lvPda";
          this.lvPda.ShowItemToolTips = true;
          this.lvPda.Size = new System.Drawing.Size(480, 143);
          this.lvPda.SmallImageList = this.imageList1;
          this.lvPda.TabIndex = 9;
          this.lvPda.UseCompatibleStateImageBehavior = false;
          this.lvPda.View = System.Windows.Forms.View.SmallIcon;
          // 
          // chPda
          // 
          this.chPda.Text = "PDA";
          // 
          // imageList1
          // 
          this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
          this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
          this.imageList1.Images.SetKeyName(0, "online.ico");
          this.imageList1.Images.SetKeyName(1, "offline.png");
          // 
          // timerUpdate
          // 
          this.timerUpdate.Interval = 10000;
          this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
          // 
          // btGrp1
          // 
          this.btGrp1.Location = new System.Drawing.Point(13, 284);
          this.btGrp1.Name = "btGrp1";
          this.btGrp1.Size = new System.Drawing.Size(75, 23);
          this.btGrp1.TabIndex = 10;
          this.btGrp1.Text = "一";
          this.btGrp1.UseVisualStyleBackColor = true;
          this.btGrp1.Click += new System.EventHandler(this.btGrp1_Click);
          // 
          // MainForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(584, 546);
          this.Controls.Add(this.btGrp1);
          this.Controls.Add(this.lvPda);
          this.Controls.Add(this.txPda2Ip);
          this.Controls.Add(this.txPda1Ip);
          this.Controls.Add(this.txLocalIp);
          this.Controls.Add(this.btPda2);
          this.Controls.Add(this.btPda1);
          this.Controls.Add(this.statusStrip);
          this.Controls.Add(this.btStop);
          this.Controls.Add(this.btStart);
          this.Controls.Add(this.picMain);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.Name = "MainForm";
          this.Text = "监视";
          this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
          this.Move += new System.EventHandler(this.MainForm_Move);
          this.Load += new System.EventHandler(this.MainForm_Load);
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
      private System.Windows.Forms.Button btPda2;
      private System.Windows.Forms.TextBox txLocalIp;
      private System.Windows.Forms.TextBox txPda1Ip;
      private System.Windows.Forms.TextBox txPda2Ip;
      private System.Windows.Forms.ListView lvPda;
      private System.Windows.Forms.ColumnHeader chPda;
      private System.Windows.Forms.Timer timerUpdate;
      private System.Windows.Forms.ImageList imageList1;
      private System.Windows.Forms.Button btGrp1;
    }
}

