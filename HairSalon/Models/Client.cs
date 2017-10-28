using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    public int Id {get; private set;}
    public string Name {get; private set;}
    public int StylistId {get; private set;}

    public Client(string name, int stylistId = 0, int id = 0)
    {
      Name = name;
      Id = id;
      StylistId = stylistId;
    }

    public static List<Client> GetAll()
    {
      List<Client> output = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string notes = rdr.GetString(2);
        int stylistId = rdr.GetInt32(4);
        Client newClient = new Client(name, stylistId, id);
        output.Add(newClient);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      Client.ClearAll();
    }
  }
}
