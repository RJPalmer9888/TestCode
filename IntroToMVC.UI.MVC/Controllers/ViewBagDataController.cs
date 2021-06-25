using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntroToMVC.UI.MVC.Models; // Allows us access to our Models

namespace IntroToMVC.UI.MVC.Controllers
{
    public class ViewBagDataController : Controller
    {
        // GET: ViewBagData
        public ActionResult Index()
        {
            //  The Viewbag is a dynamic variable.  Which means it does not exist until runtime.  
            // When we call this variable from the UI (index View) we will not get intellisense 
            // then either because it does not exist until runtime.  
            //  The ViewBag can be used to pass data between an action and its corresponding 
            //  view (via the Controller.  You CANNOT pass ViewBag objects BETWEEN actions or 
            //  different views.

            //  Any declaration of variables or calculations/activity needs to be done BEFORE 
            //  the return View();

            ViewBag.FromController = "Data from the controller";

            return View();
            //  int x = 100;
            //  This and the "x" variable above are unreachable code - 
            // uncomment the line above to see the warning.

        }
        public ActionResult Greetings(string firstName, string lastName)
        {
            //  Write logic for the INITIAL REQUEST and defaulting of our
            //  ViewBag variable if the string(s) are empty or null

            if (string.IsNullOrEmpty(firstName) && (string.IsNullOrEmpty(lastName)))
            {
                ViewBag.Greeting = null;
            }
            else
            {
                ViewBag.Greeting = "Welcome, " + firstName + " " + lastName;
            }

            return View();
        }

        public ActionResult SimpleCalculator(double? Number1, double? Number2, string function)
        {
            if (Number1 != null && Number2 != null)
            {
                switch (function)
                {
                    case "Add":
                        ViewBag.Answer = $"The result is {(Number1 + Number2)}";
                        break;

                    case "Subtract":
                        ViewBag.Answer = $"The result is {(Number1 - Number2)}";
                        break;

                    case "Multiply":
                        ViewBag.Answer = $"The result is {(Number1 * Number2)}";
                        break;

                    default:
                        if (Number2 != 0)
                        {
                            ViewBag.Answer = $"The result is {(Number1 / Number2)}";
                        }
                        else
                        {
                            ViewBag.Answer = $"Bro what.";
                        }
                        break;
                }


            }
            else
            {
                ViewBag.Answer = $"Bro gimme number.";
            }
            return View();
        }

        public ActionResult MadLibs(string tbForeignCountry, string tbAdjective, 
            string tbAnimal, string tbVerb, string tbTypeOfLiquid, string tbPlace)
        {
            //  Write logic for the INITIAL REQUEST and defaulting of our
            //  ViewBag variable if the string(s) are empty or null

            if (!string.IsNullOrEmpty(tbForeignCountry) && 
                !string.IsNullOrEmpty(tbAdjective) &&
                !string.IsNullOrEmpty(tbAnimal) &&
                !string.IsNullOrEmpty(tbVerb) &&
                !string.IsNullOrEmpty(tbTypeOfLiquid) &&
                !string.IsNullOrEmpty(tbPlace))
            {
                ViewBag.madLib = $"If you are traveling in {tbForeignCountry} " +                $"and find yourself having to cross a piranha-filled river, here’s how" +                $" to do it safely. Piranhas are more {tbAdjective} " +                $"during the day, so cross the river at night. Avoid areas with netted {tbAnimal} " +                $"traps –piranhas may be waiting there looking to {tbVerb} " +                $"them! Piranhas are attracted to fresh {tbTypeOfLiquid} " +                $"and will migrate to it as often as possible. Whatever you do, " +                $"if you have an open wound, try to find another way to get back to the " +                $"{tbPlace}.";
            }
            else
            {
                ViewBag.madLib = null;

            }

            return View();
        }

        public ActionResult ShowFamilyMember(string name, string relation)
        {
            FamilyMemberViewModel famMem = new FamilyMemberViewModel(111, name,
                relation, new DateTime(1960, 12, 25));
            ViewBag.famMem = famMem;
            return View();
        }
    }
}