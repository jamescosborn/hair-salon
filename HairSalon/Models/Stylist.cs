using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
    public int Id {get; private set;}
    public string Name {get; private set;}
    // public int Age {get; private set;}
    // public string Music {get; private set;}

    public Stylist(string name, int id = 0)
    {
      Name = name;
      // Age = age;
      // Music = music;
      Id = id;
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> output = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        // int age = rdr.GetInt32(2);
        // string music = rdr.GetString(3);
        Stylist newStylist = new Stylist(name, id);
        output.Add(newStylist);
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
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      //  Client.ClearAll();

    }
    public bool HasSamePropertiesAs(Stylist other)
    {
      return (
        this.Id == other.Id &&
        this.Name == other.Name);
    }
  }
}
