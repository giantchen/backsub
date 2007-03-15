using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class GetImage : System.Web.UI.Page
{
  static string connStr = ConfigurationManager.ConnectionStrings["Custom"].ConnectionString;
  protected void Page_Load(object sender, EventArgs e)
  {
    byte[] data = null;
    DateTime date = DateTime.Now;
    using (SqlConnection conn = new SqlConnection(connStr))
    {
      SqlCommand command = new SqlCommand("SELECT [Image], [TimeStamp] FROM [Images] WHERE Id = @Id", conn);
      string queryId = Request.QueryString["Id"];
      if (string.IsNullOrEmpty(queryId))
      {
        Label1.Text = "È±ÉÙ²ÎÊý£¡";
      }
      else
      {
        long imageId = long.Parse(queryId);
        command.Parameters.AddWithValue("@Id", imageId);
        command.Connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
          data = (byte[])reader["Image"];
          date = (DateTime)reader["TimeStamp"];
        }
      }
    }
    
    if (data != null)
    {
      // Set the page's content type to JPEG files
      // and clear all response headers.
      Response.ContentType = "image/jpeg";
      Response.AddHeader("Last-Modified", date.ToUniversalTime().ToString("r"));
      Response.Clear();

      // Buffer response so that page is sent
      // after processing is complete.
      Response.BufferOutput = true;
      
      // Send the image data.
      Response.OutputStream.Write(data, 0, data.Length);
      
      // Send the output to the client.
      Response.Flush();
      Response.End();
    }
  }
}
