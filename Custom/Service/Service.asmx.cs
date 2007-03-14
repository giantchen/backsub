using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// <summary>
    /// A test method.
    /// </summary>
    /// <returns>A fix string of "Hello World"</returns>
    [WebMethod]
    public string HelloWorld()
    {
      return "Hello World";
    }
    
    [WebMethod]
    public Pda[] ListAlivePdas()
    {
      /*
      return new Pda[]{new Pda("pda1", "192.168.0.1", DateTime.Now), 
                      new Pda("pda2", "192.168.0.2", DateTime.Now)};
      */
      SqlConnection conn = new SqlConnection(connStr);
      List<Pda> pdas= new List<Pda>();
      try
      {
        conn.Open();
        SqlCommand command = new SqlCommand("SELECT * FROM PdaState ", conn);
        //SqlDataReader reader = command.ExecuteReader();
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          string name = (string) reader["Pda"];
          Object o = reader["LastUpdate"];
          DateTime time = (DateTime) reader["LastUpdate"];
          string ip = (string) reader["IpAddr"];
          Pda p = new Pda(name, ip, time);
          pdas.Add(p);
        }
      }
      finally
      {
        conn.Close();
      }
      //return count;
      return pdas.ToArray();
    }
    
    //[WebMethod]
    [WebMethod]
    public int UpdatePda(string pdaName, string ipAddr)
    {
      SqlConnection conn = new SqlConnection(connStr);
      int count = -1;
      try
      {
        conn.Open();
        SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM PdaState ", conn);
        //SqlDataReader reader = command.ExecuteReader();
        count = (int)command.ExecuteScalar();
      } finally
      {
        conn.Close();
      }
      return count;
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
