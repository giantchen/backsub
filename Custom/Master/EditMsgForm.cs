using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Master
{
  public partial class EditMsgForm : Form
  {
    public EditMsgForm()
    {
      InitializeComponent();
    }

    private void EditMsgForm_Load(object sender, EventArgs e)
    {
      try
      {
        string[] msgs = File.ReadAllLines(Application.StartupPath + "\\messages.txt",
                                          Encoding.Default);
        comboBox1.Items.Clear();
        comboBox1.Items.AddRange(msgs);
      }
      catch (Exception ex)
      {
        
      }
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      textBox1.Text = comboBox1.SelectedItem.ToString();
    }
  }
}