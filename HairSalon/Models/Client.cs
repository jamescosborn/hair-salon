using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    public string Name {get; private set;}
    public int Id {get; private set;}

    public Client(string name, int id = 0)
    {
      Name = name;
      Id = id;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.Id == newClient.Id);
        bool nameEquality = (this.Name == newClient.Name);
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

    public bool HasSamePropertiesAs(Client other)
    {
      return (
        this.Id == other.Id &&
        this.Name == other.Name);
    }

    // CREATE

    public void Save()
     {
       MySqlConnection conn = DB.Connection();
       conn.Open();

       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"INSERT INTO clients (name) VALUES (@name);";

       MySqlParameter name = new MySqlParameter();
       name.ParameterName = "@name";
       name.Value = this.Name;
       cmd.Parameters.Add(name);

       cmd.ExecuteNonQuery();
       Id = (int) cmd.LastInsertedId;

       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
     }
    // READ

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>();

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);

        Client newClient = new Client(clientName, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    // DESTROY

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
    }
  }
}
