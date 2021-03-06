using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace BandTracker.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

      [HttpGet("/bands")]
      public ActionResult Bands()
      {
        Dictionary<string, object> model = new Dictionary<string,object>{};

        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());
        return View(model);
      }

      [HttpPost("/bands")]
      public ActionResult BandsPostNewBand()
      {

        Band newBand = new Band(Request.Form["name"], Request.Form["genre"], Request.Form["image"]);
        newBand.Save();

        Dictionary<string, object> model = new Dictionary<string,object>{};

        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());
        return View("bands",model);
      }

      [HttpGet("/venues")]
      public ActionResult Venues()
      {
        Dictionary<string, object> model = new Dictionary<string,object>{};

        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());
        return View(model);
      }

      [HttpPost("/venues")]
      public ActionResult VenuesPostNewVenue()
      {

        Venue newVenue = new Venue(Request.Form["name"], Request.Form["address"], int.Parse(Request.Form["capacity"]));
        newVenue.Save();

        Dictionary<string, object> model = new Dictionary<string,object>{};

        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());
        return View("venues",model);
      }

      [HttpPost("/bands/new-venues")]
      public ActionResult BandsAddVenues()
      {

        Band foundBand = Band.Find(int.Parse(Request.Form["bandId"]));

        string venueValues = (Request.Form["venues"]);
        string[] venueIds = venueValues.Split(',');

        foreach(var venueId in venueIds)
        {
            foundBand.AddVenue(int.Parse(venueId));
        }

        Dictionary<string, object> model = new Dictionary<string,object>{};
        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());

        return View("bands",model);
      }

      [HttpPost("/venues/new-bands")]
      public ActionResult VenuesAddBands()
      {
        Venue foundVenue = Venue.Find(int.Parse(Request.Form["venueId"]));

        string bandValues = (Request.Form["bands"]);
        string[] bandIds = bandValues.Split(',');

        foreach(var bandId in bandIds)
        {
            foundVenue.AddBand(int.Parse(bandId));
        }

        Dictionary<string, object> model = new Dictionary<string,object>{};
        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());

        return View("venues",model);
      }

      [HttpPost("/bandDetails")]
      public ActionResult BandDetails()
      {
        Band foundBand = Band.Find(int.Parse(Request.Form["bandId"]));
        return View("BandDetails",foundBand);
      }

      [HttpPost("/bands/edited")]
      public ActionResult BandDetailsEdit()
      {
        Band foundBand = Band.Find(int.Parse(Request.Form["bandId"]));
        foundBand.Update(Request.Form["name"], Request.Form["genre"], Request.Form["image"]);

        Dictionary<string, object> model = new Dictionary<string,object>{};
        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());

        return View("bands",model);
      }

      [HttpPost("/venueDetails")]
      public ActionResult VenueDetails()
      {

        Venue foundVenue = Venue.Find(int.Parse(Request.Form["venueId"]));
        return View("VenueDetails",foundVenue);
      }

      [HttpPost("/venues/edited")]
      public ActionResult VenueDetailsEdit()
      {
        Venue foundVenue = Venue.Find(int.Parse(Request.Form["venueId"]));
        foundVenue.Update(Request.Form["name"], Request.Form["address"], int.Parse(Request.Form["capacity"]));

        Dictionary<string, object> model = new Dictionary<string,object>{};
        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());

        return View("venues",model);
      }

      [HttpPost("/bands/removed")]
      public ActionResult BandsRemoved()
      {
        Band foundBand = Band.Find(int.Parse(Request.Form["bandId"]));
        foundBand.Delete();
        Dictionary<string, object> model = new Dictionary<string,object>{};

        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());
        return View("bands",model);
      }

      [HttpPost("/venues/removed")]
      public ActionResult VenuesRemoved()
      {
        Venue foundVenue = Venue.Find(int.Parse(Request.Form["venueId"]));
        foundVenue.Delete();
        Dictionary<string, object> model = new Dictionary<string,object>{};

        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());
        return View("venues",model);
      }

      [HttpPost("/bands/removeVenue")]
      public ActionResult BandsVenueRemoved()
      {
        Band foundBand = Band.Find(int.Parse(Request.Form["bandId"]));
        foundBand.RemoveVenue(int.Parse(Request.Form["venueId"]));

        Dictionary<string, object> model = new Dictionary<string,object>{};
        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());
        return View("bands",model);
      }

      [HttpPost("/venues/removeBand")]
      public ActionResult VenuesBandRemoved()
      {
        Venue foundVenue = Venue.Find(int.Parse(Request.Form["venueId"]));

        foundVenue.RemoveBand(int.Parse(Request.Form["bandId"]));

        Dictionary<string, object> model = new Dictionary<string,object>{};

        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.GetAll());
        return View("venues",model);
      }

      [HttpPost("/bands/search")]
      public ActionResult BandSearch()
      {
        string searchQuery = Request.Form["bandSearch"];
        Dictionary<string, object> model = new Dictionary<string,object>{};

        model.Add("bands", Band.Search(searchQuery));
        model.Add("venues", Venue.GetAll());
        return View("bands",model);
      }

      [HttpPost("/venues/search")]
      public ActionResult VenueSearch()
      {
        string searchQuery = Request.Form["venueSearch"];
        Dictionary<string, object> model = new Dictionary<string,object>{};

        model.Add("bands", Band.GetAll());
        model.Add("venues", Venue.Search(searchQuery));
        return View("venues",model);
      }



    }
}
