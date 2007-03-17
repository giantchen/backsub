using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Viewer.localhost;

namespace Viewer
{
  public partial class MainForm : Form
  {
    UDPListener listener_;
    Thread listenerThread_;
    internal string url = "http://fuzzy-develop/";
    const int portToListen = 8899;
    string deviceId = null;
    string pdaName = null;
    string ipAddr = null;
    Service service;
    
    public MainForm()
    {
      InitializeComponent();

      listener_ = new UDPListener(portToListen, this);
      listenerThread_ = new Thread(new ThreadStart(listener_.Run));

      string hostname = Dns.GetHostName();
      IPHostEntry ips = Dns.GetHostEntry(hostname);
      foreach (IPAddress ip in ips.AddressList)
      {
        textBox1.Text += ip.ToString() + ", ";
        if (ipAddr == null)
          ipAddr = ip.ToString();
      }

      deviceId = Program.GetDeviceID();
      // textBox2.Text += deviceId;
      service = new Service();
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
        //Application.Exit();
        this.Close();
      }
    }

    internal void btRedraw_Click(object sender, EventArgs e)
    {
      try
      {
        /*
        WebRequest wreq = WebRequest.Create(url);
        WebResponse wres = wreq.GetResponse();
        Bitmap bitmap = new Bitmap(wres.GetResponseStream());
        wres.Close();
        //wreq.
        */
        Show show = service.GetShow(pdaName);
        tbMsg.Text = show.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss    ") + show.Text;
        picMain.Image = new Bitmap(new MemoryStream(show.Image));
        picMain.Invalidate();
        Program.PlaySound(@"\Windows\notify.wav", IntPtr.Zero, 
                          (int)(Program.Flags.SND_ASYNC | Program.Flags.SND_FILENAME));
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "≥ˆœ÷“Ï≥£");
      }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      pdaName = service.RegisterPda(deviceId);
      tbMsg.Text = pdaName;
      timerUpdate_Tick(sender, e);
      timerUpdate.Enabled = true;
      listenerThread_.Start();
    }
    
    private void MainForm_Closed(object sender, EventArgs e)
    {
      UdpClient uc = new UdpClient();
      IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portToListen);
      uc.Send(Encoding.Default.GetBytes("stop"), 4, ep);
      Thread.Sleep(500);

    }

    private void timerUpdate_Tick(object sender, EventArgs e)
    {
      service.UpdatePda(deviceId, string.Format("{0}:{1}", ipAddr, portToListen));
    }
  }

  class UDPListener
  {
    private MainForm form_;
    private int portToListen_;

    public void Run()
    {
      //try
      //{
        UdpClient listener = new UdpClient(portToListen_);
      //} catch(E)
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

          if (msg.IndexOf("refresh") == 0)
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