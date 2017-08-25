using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class VenueTests : IDisposable
  {
    public VenueTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=band_tracker_test;";
    }
    public void Dispose()
    {
       Venue.DeleteAll();
       Venue.DeleteAll();
    }

    [TestMethod]
    public void GetAll_ChecksForEmptyDatabaseBeforeEntries_0()
    {
      int expected = 0;

      int actual = Venue.GetAll().Count;

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Save_SavesVenueToDatabase_VenueList()
    {
      Venue newVenue = new Venue("Key Arena", "Seattle WA", 3000);
      newVenue.Save();

      List<Venue> expected = new List<Venue> {newVenue};
      List<Venue> actual = Venue.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DeleteAll_DeletesAllVenuesFromDatabase_VenueList()
    {
      Venue newVenue = new Venue("Key Arena", "Seattle WA", 3000);
      newVenue.Save();

      List<Venue> expected = new List<Venue> {};
      Venue.DeleteAll();
      List<Venue> actual = Venue.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Find_FindVenueById_Venue()
    {
      Venue newVenue = new Venue("Key Arena", "Seattle WA", 3000);
      newVenue.Save();

      Venue expected = newVenue;
      Venue actual =  Venue.Find(newVenue.GetId());

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeleteVenue_ListVenues()
    {
      Venue newVenue = new Venue("Key Arena", "Seattle WA", 3000);
      newVenue.Save();

      Venue newVenue2 = new Venue("The Crocodile", "Seattle WA", 200);
      newVenue2.Save();
      newVenue2.Delete();

      List<Venue> expected = new List<Venue>{newVenue};
      List<Venue> actual = Venue.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
