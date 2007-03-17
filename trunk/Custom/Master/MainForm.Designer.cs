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
          this.txLocalIp = new System.Windows.Forms.TextBox();
          this.lvPda = new System.Windows.Forms.ListView();
          this.chPda = new System.Windows.Forms.ColumnHeader();
          this.imageList1 = new System.Windows.Forms.ImageList(this.components);
          this.timerUpdate = new System.Windows.Forms.Timer(this.components);
          this.btGrpAll = new System.Windows.Forms.Button();
          this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
          this.label1 = new System.Windows.Forms.Label();
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
          this.btStart.TabIndex = 2;
          this.btStart.Text = "Start";
          this.btStart.UseVisualStyleBackColor = true;
          this.btStart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btStart_MouseClick);
          // 
          // btStop
          // 
          this.btStop.Location = new System.Drawing.Point(12, 41);
          this.btStop.Name = "btStop";
          this.btStop.Size = new System.Drawing.Size(75, 23);
          this.btStop.TabIndex = 3;
          this.btStop.Text = "Stop";
          this.btStop.UseVisualStyleBackColor = true;
          this.btStop.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btStop_MouseClick);
          // 
          // statusStrip
          // 
          this.statusStrip.Location = new System.Drawing.Point(0, 524);
          this.statusStrip.Name = "statusStrip";
          this.statusStrip.Size = new System.Drawing.Size(584, 22);
          this.statusStrip.TabIndex = 4;
          this.statusStrip.Text = "statusStrip";
          // 
          // timerMove
          // 
          this.timerMove.Interval = 1000;
          this.timerMove.Tick += new System.EventHandler(this.timerMove_Tick);
          // 
          // txLocalIp
          // 
          this.txLocalIp.Location = new System.Drawing.Point(12, 70);
          this.txLocalIp.Multiline = true;
          this.txLocalIp.Name = "txLocalIp";
          this.txLocalIp.Size = new System.Drawing.Size(75, 33);
          this.txLocalIp.TabIndex = 1;
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
          // btGrpAll
          // 
          this.btGrpAll.Location = new System.Drawing.Point(12, 142);
          this.btGrpAll.Name = "btGrpAll";
          this.btGrpAll.Size = new System.Drawing.Size(75, 23);
          this.btGrpAll.TabIndex = 10;
          this.btGrpAll.Text = "全部";
          this.btGrpAll.UseVisualStyleBackColor = true;
          this.btGrpAll.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btGrpAll_MouseClick);
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(13, 124);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(53, 12);
          this.label1.TabIndex = 11;
          this.label1.Text = "发送至：";
          // 
          // MainForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(584, 546);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.btGrpAll);
          this.Controls.Add(this.lvPda);
          this.Controls.Add(this.txLocalIp);
          this.Controls.Add(this.statusStrip);
          this.Controls.Add(this.btStop);
          this.Controls.Add(this.btStart);
          this.Controls.Add(this.picMain);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
          this.KeyPreview = true;
          this.MaximizeBox = false;
          this.Name = "MainForm";
          this.Text = "监视";
          this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
          this.Move += new System.EventHandler(this.MainForm_Move);
          this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
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
      private System.Windows.Forms.TextBox txLocalIp;
      private System.Windows.Forms.ListView lvPda;
      private System.Windows.Forms.ColumnHeader chPda;
      private System.Windows.Forms.Timer timerUpdate;
      private System.Windows.Forms.ImageList imageList1;
      private System.Windows.Forms.Button btGrpAll;
      private System.Windows.Forms.ToolTip toolTip1;
      private System.Windows.Forms.Label label1;
    }
}

