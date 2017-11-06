using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Models;
using HairSalon.ViewModels;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Stylist> model = Stylist.GetAll();
      return View(model);
    }

    [HttpPost("/stylists/add")]
    public ActionResult AddStylist()
    {
      string stylistName = Request.Form["stylist-name"];
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();

      List<Stylist> model = Stylist.GetAll();

      return View("Index", model);
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult StylistDetails(int id)
    {
      StylistDetailsModel model = new StylistDetailsModel(id);
      return View(model);
    }

    [HttpPost("/stylists/{stylistId}/clients/add")]
    public ActionResult AddClientToStylist(int stylistId)
    {
      string clientName = Request.Form["client-name"];

      Client newClient = new Client(clientName, stylistId);
      newClient.Save();

      StylistDetailsModel model = new StylistDetailsModel(stylistId);
      return View("StylistDetails", model);
    }
  }
}
