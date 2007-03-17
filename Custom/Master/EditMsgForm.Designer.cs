namespace Master
{
  partial class EditMsgForm
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
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.btOK = new System.Windows.Forms.Button();
      this.btCancel = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(12, 71);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(267, 72);
      this.textBox1.TabIndex = 0;
      // 
      // comboBox1
      // 
      this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(12, 24);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(267, 20);
      this.comboBox1.TabIndex = 1;
      this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
      // 
      // btOK
      // 
      this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btOK.Location = new System.Drawing.Point(12, 188);
      this.btOK.Name = "btOK";
      this.btOK.Size = new System.Drawing.Size(75, 23);
      this.btOK.TabIndex = 2;
      this.btOK.Text = "确定发送";
      this.btOK.UseVisualStyleBackColor = true;
      // 
      // btCancel
      // 
      this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btCancel.Location = new System.Drawing.Point(93, 188);
      this.btCancel.Name = "btCancel";
      this.btCancel.Size = new System.Drawing.Size(75, 23);
      this.btCancel.TabIndex = 3;
      this.btCancel.Text = "取消";
      this.btCancel.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.SystemColors.Window;
      this.label1.Location = new System.Drawing.Point(12, 149);
      this.label1.Margin = new System.Windows.Forms.Padding(3);
      this.label1.Name = "label1";
      this.label1.Padding = new System.Windows.Forms.Padding(2);
      this.label1.Size = new System.Drawing.Size(267, 32);
      this.label1.TabIndex = 4;
      this.label1.Text = "图片及消息将发往";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(10, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(65, 12);
      this.label2.TabIndex = 5;
      this.label2.Text = "预设消息：";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(10, 55);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(65, 12);
      this.label3.TabIndex = 6;
      this.label3.Text = "编辑消息：";
      // 
      // EditMsgForm
      // 
      this.AcceptButton = this.btOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btCancel;
      this.ClientSize = new System.Drawing.Size(292, 223);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btCancel);
      this.Controls.Add(this.btOK);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.textBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "EditMsgForm";
      this.Text = "编辑消息";
      this.Load += new System.EventHandler(this.EditMsgForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.Button btOK;
    private System.Windows.Forms.Button btCancel;
    internal System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    internal System.Windows.Forms.Label label1;
  }
}