using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Service
{
  /// <summary>
  /// Summary description for Service1
  /// </summary>
  [WebService(Namespace = "http://fuzzy.bnu.edu.cn/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [ToolboxItem(false)]
  public class Service : System.Web.Services.WebService
  {
    static string connStr = ConfigurationManager.ConnectionStrings["Custom"].ConnectionString;
    
    [WebMethod]
    public Pda[] ListAllPdas()
    {
      List<Pda> pdas= new List<Pda>();
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        conn.Open();
        SqlCommand command = new SqlCommand("SELECT * FROM PdaState ", conn);
        //SqlDataReader reader = command.ExecuteReader();
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          string name = (string)reader["Pda"];
          DateTime time = (DateTime)reader["LastUpdate"];
          string ip = (string)reader["IpAddr"];
          string owner = reader["Owner"] as string;
          string unit = reader["Unit"] as string;
          if (owner == null)
            owner = "";
          if (unit == null)
            unit = "";

          Pda p = new Pda(name, ip, time, owner, unit);
          pdas.Add(p);
        }
      }

      return pdas.ToArray();
    }
    
    [WebMethod]
    public int UpdatePda(string deviceId, string ipAddr)
    {
      int count = -1;
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        //try
        //{
        SqlCommand command = new SqlCommand("UPDATE PdaState SET LastUpdate = @LastUpdate, IpAddr = @IpAddr WHERE DeviceId = @DeviceId", conn);
        command.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
        command.Parameters.AddWithValue("@IpAddr", ipAddr);
        command.Parameters.AddWithValue("@DeviceId", deviceId);
        command.Connection.Open();
        count = command.ExecuteNonQuery();

        /*
        if (count == 0)
        {
          command = new SqlCommand("INSERT INTO PdaState (Pda, IpAddr, LastUpdate) VALUES (@Pda, @IpAddr, @LastUpdate)", conn);
          command.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
          command.Parameters.AddWithValue("@IpAddr", ipAddr);
          command.Parameters.AddWithValue("@Pda", pdaName);
          count = command.ExecuteNonQuery();
        }
        */
        //}
        /*
        catch (Exception ex)
        {
          Debug.WriteLine(ex);
        }
        */
      } 
      return count;
    }

    [WebMethod]
    public string RegisterPda(string deviceId)
    {
      string pdaName = null;
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        SqlCommand command = new SqlCommand("SELECT [Pda] FROM PdaState WHERE DeviceId = @DeviceId", conn);
        command.Parameters.AddWithValue("@DeviceId", deviceId);
        command.Connection.Open();
        object ret = command.ExecuteScalar();

        if (ret != null)
        {
          pdaName = (string) ret;
        }
        else
        {
          command = new SqlCommand(
            "INSERT INTO PdaState (DeviceId) VALUES (@DeviceId); " +
            "UPDATE PdaState SET [Pda] = 'PDA_'+RIGHT('00'+LTRIM((SELECT COUNT(*) FROM [PdaState])), 2) WHERE Id = @@IDENTITY;" +
            "SELECT [Pda] FROM PdaState WHERE DeviceId = @DeviceId"
            ,conn);
          command.Parameters.AddWithValue("@DeviceId", deviceId);
          ret = command.ExecuteScalar();

          if (ret != null)
          {
            pdaName = (string)ret;
          }
        }
      }
      return pdaName;
    }
    
    [WebMethod]
    public long AddImage(byte[] date)
    {
      long imageId = 0;
      
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        SqlCommand command = new SqlCommand("SET NOCOUNT ON; INSERT INTO Images ([Image], [TimeStamp]) VALUES (@Image, @TimeStamp); SELECT @@IDENTITY", conn);
        command.Parameters.AddWithValue("@TimeStamp", DateTime.Now);
        command.Parameters.Add("@Image", SqlDbType.Image);
        command.Parameters["@Image"].Value = date;
        command.Connection.Open();
        imageId = (long)(decimal)command.ExecuteScalar();
      }
      
      return imageId;
    }
    
    [WebMethod]
    public byte[] GetImage(long imageId)
    {
      byte[] data = null;
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        SqlCommand command = new SqlCommand("SELECT [Image] FROM Images WHERE [Id] = @Id", conn);
        command.Parameters.AddWithValue("@Id", imageId);
        command.Connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
          data = (byte[])reader["Image"];
        }
      }

      return data;
    }
  
    [WebMethod]
    public int SendImage(long imageId, string text, string[] pda)
    {
      int count;
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        StringBuilder sb = new StringBuilder();
        foreach (string p in pda)
        {
          sb.AppendFormat("INSERT INTO [Shows] ([Pda], [ImageId], [TimeStamp], [Text]) VALUES ('{0}', {1}, @TimeStamp, @Text); ", p, imageId);
        }
        SqlCommand command = new SqlCommand(sb.ToString(), conn);
        command.Parameters.AddWithValue("@TimeStamp", DateTime.Now);
        command.Parameters.AddWithValue("@Text", text);
        command.Connection.Open();
        count = command.ExecuteNonQuery();
      }
      return count;
    }
    
    [WebMethod]
    public Show GetShow(string pda)
    {
      Show show = null;
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        SqlCommand command = new SqlCommand("SELECT TOP 1 [Images].[Image], [Shows].[TimeStamp], [Shows].[Text] FROM [Images], [Shows] " + 
          "WHERE [Images].[Id] = [Shows].[ImageId] AND [Shows].[Pda] = @Pda ORDER BY [Shows].[Id] DESC", conn);
        command.Parameters.AddWithValue("@Pda", pda);
        command.Connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
          show = new Show();
          show.Image = (byte[])reader["Image"];
          show.TimeStamp = (DateTime)reader["TimeStamp"];
          show.Text = (string)reader["Text"];
        }
      }      
      return show;
    }
  }

  public class Show
  {
    public byte[] Image;
    public DateTime TimeStamp;
    public string Text;
  }
  
  public class Pda
  {
    public string Name;
    public string IpAddr;
    public DateTime LastUpdate;
    public string Owner;
    public string Unit;
    
    public Pda(string name, string ipAddr, DateTime lastUpdate, string owner, string unit)
    {
      Name = name;
      IpAddr = ipAddr;
      LastUpdate = lastUpdate;
      Owner = owner;
      Unit = unit;
    }
    
    public Pda()
    {
      
    }
  }
  
}
