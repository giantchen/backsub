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
    }

    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      int result = OkApi.CloseBoard(hBoard);
      Console.WriteLine(result);
    }

    private void btStart_Click(object sender, EventArgs e)
    {
      bool ret = startCapture();
      Console.WriteLine(ret);
      if (ret)
      {
        isActive = true;
      }
    }

    private bool startCapture()
    {
      OkApi.SetToWndRect(hBoard, picMain.Handle);
      int ret = OkApi.CaptureToScreen(hBoard);
      return ret > 0;
    }

    private void btStop_Click(object sender, EventArgs e)
    {
      stopCapture();
      isActive = false;
      saveFile();
    }

    private void stopCapture()
    {
      OkApi.StopCapture(hBoard);
    }

    private void saveFile()
    {
      basefilename = string.Format(@"{0}.jpg", DateTime.Now.ToString("yyyyMMdd_HHmmss_ffffff"));
      String filename = @"\figs\" + basefilename;
      int filesize = OkApi.SaveImageFile(hBoard, filename,
        80, OkApi.TARGET_SCREEN, 0, 1);
      if (filesize > 0)
      {
        FileStream fs = File.OpenRead(filename);
        Bitmap picture = new Bitmap((Stream)fs);
        picMain.Image = (Image)picture;
        fs.Close();
        //File.Delete(filename);
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

    private void btPda1_Click(object sender, EventArgs e)
    {
      sendtoPda(txPda1Ip.Text);
    }
    
    private void btPda2_Click(object sender, EventArgs e)
    {
      sendtoPda(txPda2Ip.Text);
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      btStart_Click(sender, e);
    }

    private void timerUpdate_Tick(object sender, EventArgs e)
    {
      Pda[] pdas = service.ListAllPdas();
      foreach (Pda p in pdas)
      {
        ListViewItem item = new ListViewItem(p.Name);

        item.ToolTipText = p.IpAddr;
        if (DateTime.Now.Subtract(p.LastUpdate).TotalMinutes < 5)
        {
          item.Group = lvPda.Groups["Online"];
          item.BackColor = Color.Pink;
        }
        else
        {
          item.Group = lvPda.Groups["Offline"];
          item.BackColor = Color.Gray;
        }
        lvPda.Items.Add(item);
      }
    }
  }
}