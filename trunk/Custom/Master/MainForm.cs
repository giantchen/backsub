using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Master.localhost;

namespace Master
{
  public partial class MainForm : Form
  {
    UdpClient udpClient_ = new UdpClient();
    IntPtr hBoard;
    bool isActive = false;
    private string basefilename;
    Service service = new Service();
    byte[] currentImage=null;
    Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>();
    EditMsgForm editMsgForm = new EditMsgForm();
    
    public MainForm()
    {
      InitializeComponent();
      int index = -1;
      hBoard = OkApi.OpenBoard(ref index);
      Debug.Assert(hBoard.ToInt32() != 0);

      OkApi.SetCaptureParam(hBoard, (ushort)ECapture.CAPTURE_CLIPMODE, 0); // 缩放方式
      string hostname = Dns.GetHostName();
      IPHostEntry ips = Dns.GetHostEntry(hostname);
      foreach (IPAddress ip in ips.AddressList)
      {
        txLocalIp.Text += ip.ToString() + "\n";
      }

      addGroupButtons();
      timerUpdate.Enabled = true;
    }

    private void addGroupButtons()
    {
      string fn = Application.StartupPath + "\\groups.txt";
      try
      {
        string[] grps = File.ReadAllLines(fn, Encoding.Default);
        foreach (string line in grps)
        {
          if (!line.Contains("="))
            continue;
          string[] fields = line.Split('=');
          string grpName = fields[0].Trim();
          string[] pdas = fields[1].Split(',');
          List<string> pdalist = new List<string>();
          foreach (string pda in pdas)
          {
            int num = int.Parse(pda);
            if (num != 0)
              pdalist.Add(string.Format("PDA_{0:D2}", num));
          }
          groups.Add(grpName, pdalist);
          int cnt = groups.Count;
          Button bt = new Button();
          bt.Location = new Point(12, btGrpAll.Location.Y+cnt*29);
          bt.Name = string.Format("grp_{0:D2}", cnt);
          bt.Size = new Size(btGrpAll.Size.Width, btGrpAll.Size.Height);
          bt.Text = grpName;
          toolTip1.SetToolTip(bt, string.Join(", ", pdalist.ToArray()));
          bt.UseVisualStyleBackColor = true;
          bt.MouseClick += new MouseEventHandler(this.btGrp_MouseClick);
          Controls.Add(bt);
        }
      }
      catch(Exception e)
      {
        MessageBox.Show(string.Format("找不到 {0} 或文件格式有误！",fn), "出错",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      //btStart_Click(sender, e);
      btStart_MouseClick(sender, null);
      //lvPda.Items.Add("PDA_5", 1);
      //lvPda.Items.Add("PDA_6", 1);
      timerUpdate_Tick(sender, e);
    }

    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      int result = OkApi.CloseBoard(hBoard);
      Console.WriteLine(result);
    }

    private bool startCapture()
    {
      OkApi.SetToWndRect(hBoard, picMain.Handle);
      int ret = OkApi.CaptureToScreen(hBoard);
      return ret > 0;
    }

    private void stopCapture()
    {
      OkApi.StopCapture(hBoard);
    }

    private void saveFile()
    {
      basefilename = string.Format(@"{0}.jpg", DateTime.Now.ToString("yyyyMMdd_HHmmss_ffffff"));
      String filename = Application.StartupPath+"\\" + basefilename;
      int filesize = OkApi.SaveImageFile(hBoard, filename,
        80, OkApi.TARGET_SCREEN, 0, 1);
      if (filesize > 0)
      {
        currentImage = File.ReadAllBytes(filename);
        FileStream fs = File.OpenRead(filename);
        Bitmap picture = new Bitmap(fs);
        picMain.Image = (Image)picture;
        fs.Close();
        File.Delete(filename);
      }
      else
      {
        Console.WriteLine(OkApi.GetLastError(hBoard));
      }
    }

    private void MainForm_Move(object sender, EventArgs e)
    {
      if (isActive)
      {
        stopCapture();
        // startCapture();
        timerMove.Start();
      }
    }

    private void timerMove_Tick(object sender, EventArgs e)
    {
      startCapture();
    }

    /*
    private void sendtoPda(string ip)
    {
      if (isActive)
      {
        stopCapture();
        saveFile();
        startCapture();
      }

      if (String.IsNullOrEmpty(basefilename))
      {
        MessageBox.Show("请先选择一幅图片");
      }
      else
      {
        string url = string.Format("http://{0}/figs/{1}", 
                                   txLocalIp.Text.Split('\n')[0], basefilename);
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), 8899);
        udpClient_.Send(Encoding.Default.GetBytes(url), url.Length, ep);
      }
    }
    */
    
    private void notifyPda(string pda)
    {
      byte[] data = Encoding.Default.GetBytes("refresh");
      ListViewItem item = lvPda.Items[pda];
      if (item != null)
      {
        string[] fields = ((string)item.Tag).Split(':');
        string ip = fields[0];
        int port = int.Parse(fields[1]);
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
        udpClient_.Send(data, data.Length, ep);
      }
    }
    /*
    private void btPda1_Click(object sender, EventArgs e)
    {
      sendtoPda(txPda1Ip.Text);
    }
    
    private void btPda2_Click(object sender, EventArgs e)
    {
      sendtoPda(txPda2Ip.Text);
    }
    */
    private void timerUpdate_Tick(object sender, EventArgs e)
    {
      Pda[] pdas = service.ListAllPdas();
      //lvPda.Items.Clear();
      foreach (Pda p in pdas)
      {
        ListViewItem item;
        if (!lvPda.Items.ContainsKey(p.Name))
        {
          //item = new ListViewItem(p.Name);
          lvPda.Items.Add(p.Name, p.Name, -1);
          //lvPda.Items.Add(p.Name);
        }

        item = lvPda.Items[p.Name];
        item.ToolTipText = string.Format("{0}, {1}", p.Owner, p.Unit);
        item.Tag = p.IpAddr;
        if (DateTime.Now.Subtract(p.LastUpdate).TotalMinutes < 2)
        {
          item.Group = lvPda.Groups["Online"];
          item.ImageIndex = 0;
          item.BackColor = Color.Gold;
        }
        else
        {
          item.Group = lvPda.Groups["Offline"];
          item.ImageIndex = 1;
          item.BackColor = Color.White;
        }
      }
      
    }

    private void btGrp_MouseClick(object sender, MouseEventArgs e)
    {
      string grpName = ((Button)sender).Text;
      sendPda(groups[grpName].ToArray(), grpName);
    }

    private void sendPda(string[] pdas, string groupName)
    {
      if (isActive)
      {
        stopCapture();
        saveFile();
      }

      if (String.IsNullOrEmpty(basefilename))
      {
        MessageBox.Show("请先选择一幅图片");
      }
      else
      {
        editMsgForm.label1.Text = string.Format("图片及消息将发往 {0}", groupName);
        DialogResult result = editMsgForm.ShowDialog();
        if (result == DialogResult.OK)
        {
          service.SendImage(currentImage, editMsgForm.textBox1.Text, pdas);
          foreach (string pda in pdas)
          {
            notifyPda(pda);
          }
        }
      }
      
      if (isActive)
        startCapture();
    }

    private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar==32)
      {
        if (isActive)
          btStop_MouseClick(sender, null);
        else
          btStart_MouseClick(sender, null);
        
        e.Handled = true;
      }
    }

    private void btGrpAll_MouseClick(object sender, MouseEventArgs e)
    {
      Pda[] pdas = service.ListAllPdas();
      string[] p = new string[pdas.Length];
      for (int i = 0; i < p.Length; ++i)
        p[i] = pdas[i].Name;

      sendPda(p, btGrpAll.Text);
    }

    private void btStart_MouseClick(object sender, MouseEventArgs e)
    {
      bool ret = startCapture();
      if (ret)
      {
        isActive = true;
      }
    }

    private void btStop_MouseClick(object sender, MouseEventArgs e)
    {
      stopCapture();
      isActive = false;
      saveFile();
    }
  }
}