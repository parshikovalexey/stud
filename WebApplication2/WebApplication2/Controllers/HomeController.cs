using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            GetDataController c1 = new GetDataController();
            var w1 = new WeightModels();
            w1.Date = DateTime.Now;
            w1.TitleOfItem = "Item";
            w1.UnitOfWeight = "Unit";
            w1.Weight = 20;
            var v1 = c1.GetXml(w1);
            var v2 = c1.GetJson(w1);

            //HttpResponseMessage v3 = c1.GetWeightings();
            return View();
        }
    }
}
