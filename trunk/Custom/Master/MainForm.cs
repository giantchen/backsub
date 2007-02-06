using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Master
{
  public partial class MainForm : Form
  {
    IntPtr hBoard;
    bool isActive = false;

    public MainForm()
    {
      InitializeComponent();
      int index = -1;
      hBoard = OkApi.OpenBoard(ref index);
      Debug.Assert(hBoard.ToInt32() != 0);

      OkApi.SetCaptureParam(hBoard, (ushort)ECapture.CAPTURE_CLIPMODE, 0); // Ëõ·Å·½Ê½
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
      string filename = "temp.jpg";
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
  }
}