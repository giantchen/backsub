using System;
using System.Collections.Generic;
using System.Text;

namespace Master
{
  public class OkApi
  {
    public const int TARGET_SCREEN = -1;

    [System.Runtime.InteropServices.DllImport("okapi32.DLL", EntryPoint = "okOpenBoard", SetLastError = true)]
    public static extern IntPtr OpenBoard(ref int index);

    [System.Runtime.InteropServices.DllImport("okapi32.DLL", EntryPoint = "okCloseBoard", SetLastError = true)]
    public static extern int CloseBoard(IntPtr handle);

    [System.Runtime.InteropServices.DllImport("okapi32.DLL", EntryPoint = "okGetLastError", SetLastError = true)]
    public static extern int GetLastError(IntPtr handle);

    [System.Runtime.InteropServices.DllImport("okapi32.DLL", EntryPoint = "okGetTypeCode", SetLastError = true)]
    public static extern short GetTypeCode(IntPtr handle, StringBuilder boardName);

    [System.Runtime.InteropServices.DllImport("okapi32.DLL", EntryPoint = "okSetCaptureParam", SetLastError = true)]
    public static extern int SetCaptureParam(IntPtr handle, ushort wParam, int lParam);

    [System.Runtime.InteropServices.DllImport("okapi32.DLL", EntryPoint = "okSetToWndRect", SetLastError = true)]
    public static extern int SetToWndRect(IntPtr handle, IntPtr hwnd);

    [System.Runtime.InteropServices.DllImport("okapi32.DLL", EntryPoint = "okCaptureToScreen", SetLastError = true)]
    public static extern int CaptureToScreen(IntPtr handle);

    [System.Runtime.InteropServices.DllImport("okapi32.DLL", EntryPoint = "okStopCapture", SetLastError = true)]
    public static extern int StopCapture(IntPtr handle);

    [System.Runtime.InteropServices.DllImport("okapi32.DLL", EntryPoint = "okSaveImageFile", SetLastError = true)]
    public static extern int SaveImageFile(IntPtr handle, string filename, int first, int source,int start, int num);
  }

  public enum ECapture
  {
    CAPTURE_RESETALL = 0,           //reset all to sys default
    CAPTURE_INTERVAL = 1,
    CAPTURE_CLIPMODE = 2,           //LOWORD: clip mode when video and dest rect not same size
                                    //exHiWord: if captrure odd and even field crosslly
    CAPTURE_SCRRGBFORMAT = 3,       //when return, loword=code, exHiWord=bits
    CAPTURE_BUFRGBFORMAT = 4,
    CAPTURE_FRMRGBFORMAT = 5,
    CAPTURE_BUFBLOCKSIZE = 6,       //lParam=MAKELONG(width,height)
                                    //if set it 0 (default), the rect set by user will be as block size
    CAPTURE_HARDMIRROR = 7,         //bit0 x, bit1 y;
    CAPTURE_VIASHARPEN = 8,         //sample via sharpen filter
    CAPTURE_VIAKFILTER = 9,         //sample via recursion filter
    CAPTURE_SAMPLEFIELD = 10,       //0 in field (non-interlaced), 1 in frame (interlaced), (0,1 are basic)
                                    //2 in field but keep expend row,3 in field but interlaced one frame
                                    //(2,4 can affect only sampllng field(frame) by field(frame) )
                                    //in 3 up-dn frame
    CAPTURE_HORZPIXELS = 11,        // set max horz pixel per scan line
    CAPTURE_VERTLINES = 12,         // set max vert lines per frame
    
    CAPTURE_ARITHMODE = 13,         //arithmatic mode
    CAPTURE_TO8BITMODE = 14,        //the mode of high (eg. 10 bits) converted to 8bit
                                    //exHiWord(lParam)=0: linear scale,
                                    //exHiWord(lParam)!=0:clip mode, LOWORD(lParam)=offset
    CAPTURE_SEQCAPWAIT = 15,        // bit0 if waiting finished for functions of sequence capturing and playbacking
                                    //bit1 if waiting finished capture then call callback function
    
    CAPTURE_MISCCONTROL = 16,       //miscellaneous control bits
                                    //bit0: 1: take one by one |okCapturByBuffer,okGetSeqCapture by interrupt control
                                    //bit1: 1: take last one   |
                                    //bit2: 1: one by one for usb20
                                   
    CAPTURE_TRIGCAPTURE = 17,       //set triggered capture, LOWORD cap no of fields, HIWORD delay fields after trigger
    CAPTURE_TURNCHANNELS = 18,      //turn channel when sequence capture
                                    //b0~6 for turn number (max 127),
                                    //b8~31(24) mask 0~23 channles, b7=1 keep this pos
    CAPTURE_EXPOSETIME = 20,        //set exposed time for camera in microsecond
  }
}
