using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Viewer
{
  public partial class MainForm : Form
  {
    UDPListener listener_;
    Thread listenerThread_;
    internal string url = "http://fuzzy-develop/";
    const int portToListen = 8899;
    
    public MainForm()
    {
      InitializeComponent();

      listener_ = new UDPListener(portToListen, this);
      listenerThread_ = new Thread(new ThreadStart(listener_.Run));
      listenerThread_.Start();
      string hostname = Dns.GetHostName();
      IPHostEntry ips = Dns.GetHostEntry(hostname);
      foreach (IPAddress ip in ips.AddressList)
      {
        textBox1.Text += ip.ToString() + ", ";
      }
    }

    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
      if ((e.KeyCode == Keys.Up))
      {
        // Rocker Up
        // Up
      }
      if ((e.KeyCode == Keys.Down))
      {
        // Rocker Down
        // Down
      }
      if ((e.KeyCode == Keys.Left))
      {
        // Left
      }
      if ((e.KeyCode == Keys.Right))
      {
        // Right
      }
      if ((e.KeyCode == Keys.Enter))
      {
        // Enter
        UdpClient uc = new UdpClient();
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portToListen);
        uc.Send(Encoding.Default.GetBytes("stop"), 4, ep);
        Thread.Sleep(500);
        Application.Exit();
      }
    }

    internal void btRedraw_Click(object sender, EventArgs e)
    {
      try
      {
        WebRequest wreq = WebRequest.Create(url);
        WebResponse wres = wreq.GetResponse();
        Bitmap bitmap = new Bitmap(wres.GetResponseStream());
        wres.Close();
        //wreq.
        picMain.Image = bitmap;
        picMain.Invalidate();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "≥ˆœ÷“Ï≥£");
      }
    }
  }

  class UDPListener
  {
    private MainForm form_;
    private int portToListen_;

    public void Run()
    {
      UdpClient listener = new UdpClient(portToListen_);
      IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
      try
      {
        while (true)
        {
          //Console.WriteLine("Waiting for refresh");
          Byte[] receiveBytes = listener.Receive(ref RemoteIpEndPoint);
          string msg = Encoding.Default.GetString(receiveBytes, 0, receiveBytes.Length);

          if (msg.IndexOf("stop") == 0)
            break;

          if (msg.IndexOf("http://") != 0)
            continue;
          form_.url = msg;
          form_.Invoke(new EventHandler(form_.btRedraw_Click));
        }

      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
    }

    public UDPListener(int portToListen, MainForm form)
    {
      portToListen_ = portToListen;
      form_ = form;
    }
  }
}