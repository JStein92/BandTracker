using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BandTracker.Models
{
  public class Venue
  {
    private int _id;
    private string _name;
    private string _address;
    private int _capacity;

    public Venue(string name, string address, int capacity, int id = 0)
    {
      _name = name;
      _address = address;
      _capacity = capacity;
      _id = id;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetAddress()
    {
      return _address;
    }
    public int GetCapacity()
    {
      return _capacity;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if(!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = this.GetId() == newVenue.GetId();
        bool nameEquality = this.GetName() == newVenue.GetName();
        bool addressEquality = this.GetAddress() == newVenue.GetAddress();
        bool capacityEquality = this.GetCapacity() == newVenue.GetCapacity();
        return (idEquality && nameEquality && addressEquality && capacityEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string address = rdr.GetString(2);
        int capacity = rdr.GetInt32(3);

        Venue newVenue = new Venue(name, address, capacity, id);
        allVenues.Add(newVenue);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allVenues;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO venues (name, address, capacity) VALUES (@name, @address, @capacity);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = _name;
      cmd.Parameters.Add(name);

      MySqlParameter address = new MySqlParameter();
      address.ParameterName = "@address";
      address.Value = _address;
      cmd.Parameters.Add(address);

      MySqlParameter capacity = new MySqlParameter();
      capacity.ParameterName = "@capacity";
      capacity.Value = _capacity;
      cmd.Parameters.Add(capacity);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues; DELETE FROM bands_venues;";

      cmd.ExecuteNonQuery();
      conn.Close();

      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Venue Find (int id)
    {

      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues WHERE id = @thisId;";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = id;
      cmd.Parameters.Add(idParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int myId = 0;
      string name = "";
      string address = "";
      int capacity = 0;

      while(rdr.Read())
      {
        myId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        address = rdr.GetString(2);
        capacity = rdr.GetInt32(3);
      }
      Venue newVenue = new Venue(name, address, capacity, myId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return newVenue;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues WHERE id = @thisId; DELETE FROM bands_venues WHERE venue_id = @thisId;";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = _id;
      cmd.Parameters.Add(idParameter);

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void Update(string newName, string newAddress, int newCapacity)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE venues SET name = @name, address = @address, capacity = @capacity WHERE id = @thisId;";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = _id;
      cmd.Parameters.Add(idParameter);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = newName;
      cmd.Parameters.Add(name);

      MySqlParameter address = new MySqlParameter();
      address.ParameterName = "@address";
      address.Value = newAddress;
      cmd.Parameters.Add(address);

      MySqlParameter capacity = new MySqlParameter();
      capacity.ParameterName = "@capacity";
      capacity.Value =newCapacity;
      cmd.Parameters.Add(capacity);

      cmd.ExecuteNonQuery();

      _name = newName;
      _address = newAddress;
      _capacity = newCapacity;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddBand(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues (band_id, venue_id) VALUES (@bandId, @thisId);";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = _id;
      cmd.Parameters.Add(idParameter);

      MySqlParameter bandId = new MySqlParameter();
      bandId.ParameterName = "@bandId";
      bandId.Value = id;
      cmd.Parameters.Add(bandId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Band> GetBands()
    {
      List<Band> venueBands = new List<Band>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT bands.* FROM venues
      JOIN bands_venues ON (venues.id = bands_venues.venue_id)
      JOIN bands ON (bands_venues.band_id = bands.id)
      WHERE venues.id = @thisId;";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = _id;
      cmd.Parameters.Add(idParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string genre = rdr.GetString(2);
        string image = rdr.GetString(3);

        Band newBand = new Band(name, genre, image, id);
        venueBands.Add(newBand);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return venueBands;
    }

    public void RemoveBand(int bandId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands_venues WHERE venue_id = @thisId AND band_id = @bandId;";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = _id;
      cmd.Parameters.Add(idParameter);

      MySqlParameter bandIdParameter = new MySqlParameter();
      bandIdParameter.ParameterName = "@bandId";
      bandIdParameter.Value = bandId;
      cmd.Parameters.Add(bandIdParameter);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Venue> Search(string venueName)
    {
      List<Venue> allVenues = new List<Venue> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues WHERE name LIKE CONCAT('%',@venueName,'%');";

      MySqlParameter venueNameParameter = new MySqlParameter();
      venueNameParameter.ParameterName = "@venueName";
      venueNameParameter.Value = venueName;
      cmd.Parameters.Add(venueNameParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string address = rdr.GetString(2);
        int capacity = rdr.GetInt32(3);

        Venue newVenue = new Venue(name, address, capacity, id);
        allVenues.Add(newVenue);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allVenues;
    }


  }

}
