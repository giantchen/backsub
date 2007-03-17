using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Viewer
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [MTAThread]
    static void Main()
    {
      Application.Run(new MainForm());
    }

    public enum Flags : int
    {
      SND_SYNC = 0x0000,  /* play synchronously (default) */
      SND_ASYNC = 0x0001,  /* play asynchronously */
      SND_NODEFAULT = 0x0002,  /* silence (!default) if sound not found */
      SND_MEMORY = 0x0004,  /* pszSound points to a memory file */
      SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
      SND_NOSTOP = 0x0010,  /* don't stop any currently playing sound */
      SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
      SND_ALIAS = 0x00010000, /* name is a registry alias */
      SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
      SND_FILENAME = 0x00020000, /* name is file name */
      SND_RESOURCE = 0x00040004  /* name is resource name or atom */
    }
    
    [DllImport("coredll.dll", SetLastError = true)]
    public static extern int PlaySound(string sound, IntPtr hModule, int flags);
    
    [DllImport("coredll.dll", SetLastError = true)]
    private static extern bool KernelIoControl(Int32 dwIoControlCode,
        IntPtr lpInBuf, Int32 nInBufSize, byte[] lpOutBuf,
        Int32 nOutBufSize, ref Int32 lpBytesReturned);

    public static string GetDeviceID()
    {
      const Int32 METHOD_BUFFERED = 0;
      const Int32 FILE_ANY_ACCESS = 0;
      const Int32 FILE_DEVICE_HAL = 0x00000101;

      const Int32 ERROR_NOT_SUPPORTED = 0x32;
      const Int32 ERROR_INSUFFICIENT_BUFFER = 0x7A;

      const Int32 IOCTL_HAL_GET_DEVICEID =
        ((FILE_DEVICE_HAL) << 16) | ((FILE_ANY_ACCESS) << 14)
        | ((21) << 2) | (METHOD_BUFFERED);
      
      // Initialize the output buffer to the size of a 
      // Win32 DEVICE_ID structure.
      byte[] outbuff = new byte[256];
      Int32 dwOutBytes;
      bool done = false;

      Int32 nBuffSize = outbuff.Length;

      // Set DEVICEID.dwSize to size of buffer.  Some platforms look at
      // this field rather than the nOutBufSize param of KernelIoControl
      // when determining if the buffer is large enough.
      BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);
      dwOutBytes = 0;

      // Loop until the device ID is retrieved or an error occurs.
      while (!done)
      {
        if (KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero,
            0, outbuff, nBuffSize, ref dwOutBytes))
        {
          done = true;
        }
        else
        {
          int error = Marshal.GetLastWin32Error();
          switch (error)
          {
            case ERROR_NOT_SUPPORTED:
              throw new NotSupportedException(
                  "IOCTL_HAL_GET_DEVICEID is not supported on this device",
                  new Win32Exception(error));

            case ERROR_INSUFFICIENT_BUFFER:

              // The buffer is not big enough for the data.  The
              // required size is in the first 4 bytes of the output
              // buffer (DEVICE_ID.dwSize).
              nBuffSize = BitConverter.ToInt32(outbuff, 0);
              outbuff = new byte[nBuffSize];

              // Set DEVICEID.dwSize to size of buffer.  Some
              // platforms look at this field rather than the
              // nOutBufSize param of KernelIoControl when
              // determining if the buffer is large enough.
              BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);
              break;

            default:
              throw new Win32Exception(error, "Unexpected error");
          }
        }
      }

      // Copy the elements of the DEVICE_ID structure.
      Int32 dwPresetIDOffset = BitConverter.ToInt32(outbuff, 0x4);
      Int32 dwPresetIDSize = BitConverter.ToInt32(outbuff, 0x8);
      Int32 dwPlatformIDOffset = BitConverter.ToInt32(outbuff, 0xc);
      Int32 dwPlatformIDSize = BitConverter.ToInt32(outbuff, 0x10);
      StringBuilder sb = new StringBuilder();

      for (int i = dwPresetIDOffset;
          i < dwPresetIDOffset + dwPresetIDSize; i++)
      {
        sb.Append(String.Format("{0:X2}", (int)outbuff[i]));
      }

      sb.Append("-");

      for (int i = dwPlatformIDOffset;
          i < dwPlatformIDOffset + dwPlatformIDSize; i++)
      {
        sb.Append(String.Format("{0:X2}", (int)outbuff[i]));
      }
      return sb.ToString();
    }
  }
}