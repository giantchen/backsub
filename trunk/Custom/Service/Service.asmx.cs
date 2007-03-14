using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
          string name = (string) reader["Pda"];
          DateTime time = (DateTime) reader["LastUpdate"];
          string ip = (string) reader["IpAddr"];
          Pda p = new Pda(name, ip, time);
          pdas.Add(p);
        }
      }

      return pdas.ToArray();
    }
    
    [WebMethod]
    public int UpdatePda(string pdaName, string ipAddr)
    {
      int count = -1;
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        //try
        //{
        SqlCommand command = new SqlCommand("UPDATE PdaState SET LastUpdate = @LastUpdate, IpAddr = @IpAddr WHERE Pda = @Pda", conn);
        command.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
        command.Parameters.AddWithValue("@IpAddr", ipAddr);
        command.Parameters.AddWithValue("@Pda", pdaName);
        command.Connection.Open();
        count = command.ExecuteNonQuery();

        if (count == 0)
        {
          command = new SqlCommand("INSERT INTO PdaState (Pda, IpAddr, LastUpdate) VALUES (@Pda, @IpAddr, @LastUpdate)", conn);
          command.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
          command.Parameters.AddWithValue("@IpAddr", ipAddr);
          command.Parameters.AddWithValue("@Pda", pdaName);
          count = command.ExecuteNonQuery();
        }
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
  }
  
  public class Pda
  {
    public string Name;
    public string IpAddr;
    public DateTime LastUpdate;
    
    public Pda(string name, string ipAddr, DateTime lastUpdate)
    {
      Name = name;
      IpAddr = ipAddr;
      LastUpdate = lastUpdate;
    }
    
    public Pda()
    {
      
    }
  }
  
}
