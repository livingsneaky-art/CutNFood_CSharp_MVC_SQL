using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CutNFood.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "CutNFood, where everyone wants to be";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "CutNFood main restaurant.";

            return View();
        }
        
    }
}