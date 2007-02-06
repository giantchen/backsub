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

namespace Master
{
  public partial class MainForm : Form
  {
    UdpClient udpClient_ = new UdpClient();
    IntPtr hBoard;
    bool isActive = false;
    private string basefilename;

    public MainForm()
    {
      InitializeComponent();
      int index = -1;
      hBoard = OkApi.OpenBoard(ref index);
      Debug.Assert(hBoard.ToInt32() != 0);

      OkApi.SetCaptureParam(hBoard, (ushort)ECapture.CAPTURE_CLIPMODE, 0); // ���ŷ�ʽ
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
      String filename = @"C:\figs\" + basefilename;
      int filesize = OkApi.SaveImageFile(hBoard, filename,
        80, OkApi.TARGET_SCREEN, 0, 1);
      if (filesize > 0)
      {
        FileStream fs = File.OpenRead(filename);
        Bitmap picture = new Bitmap(fs);
        picMain.Image = picture;
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

    private void btPda1_Click(object sender, EventArgs e)
    {
      sendtoPda("127.0.0.1");
    }
    
    private void sendtoPda(string ip)
    {
      if (String.IsNullOrEmpty(basefilename))
        MessageBox.Show("����ѡ��һ��ͼƬ");
      else
      {
        string url = "http://fuzzy-develop/figs/" + basefilename;
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), 8899);
        udpClient_.Send(Encoding.Default.GetBytes(url), url.Length, ep);
      }
    }
  }
}