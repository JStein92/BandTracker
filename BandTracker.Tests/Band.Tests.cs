using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class BandTests : IDisposable
  {
    public BandTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=band_tracker_test;";
    }
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

    [TestMethod]
    public void GetAll_ChecksForEmptyDatabaseBeforeEntries_0()
    {
      int expected = 0;

      int actual = Band.GetAll().Count;

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Save_SavesBandToDatabase_BandList()
    {
      Band newBand = new Band("Beatles", "Rock", "www.image.com/image.jpg");
      newBand.Save();

      List<Band> expected = new List<Band> {newBand};
      List<Band> actual = Band.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DeleteAll_DeletesAllBandsFromDatabase_BandList()
    {
      Band newBand = new Band("Beatles", "Rock", "www.image.com/image.jpg");
      newBand.Save();

      List<Band> expected = new List<Band> {};
      Band.DeleteAll();
      List<Band> actual = Band.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Find_FindBandById_Band()
    {
      Band newBand = new Band("Beatles", "Rock", "www.image.com/image.jpg");
      newBand.Save();

      Band expected = newBand;
      Band actual =  Band.Find(newBand.GetId());

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeleteBand_ListBands()
    {
      Band newBand = new Band("Beatles", "Rock", "www.image.com/image.jpg");
      newBand.Save();

      Band newBand2 = new Band("Monkeys", "Roll", "www.test.com/image.jpg");
      newBand2.Save();
      newBand2.Delete();

      List<Band> expected = new List<Band>{newBand};
      List<Band> actual = Band.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AddVenue_AddVenueToJoinTable_ListVenues()
    {
      Band newBand = new Band("Beatles", "Rock", "www.image.com/image.jpg");
      newBand.Save();

      Venue newVenue = new Venue("Key Arena", "Seattle WA", 3000);
      newVenue.Save();

      newBand.AddVenue(newVenue.GetId());

      List<Venue> expected = new List<Venue>{newVenue};
      List<Venue> actual = newBand.GetVenues();

      CollectionAssert.AreEqual(expected,actual);
    }
  }
}
