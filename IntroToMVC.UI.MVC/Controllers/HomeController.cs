using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntroToMVC.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string area, string myName, string myJob)
        {
            ViewBag.newParam = area + " " + myName + " " + myJob;
            // ViewBag is an object with properties (aka a Model) that is passed back to the view via the Controller
            return View(ViewBag);
        }

        public ActionResult IntroToRazor()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}