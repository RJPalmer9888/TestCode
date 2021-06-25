using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntroToMVC.UI.MVC.Models;
using System.Net; //Email
using System.Configuration; //Email
using System.Net.Mail; //Email

namespace IntroToMVC.UI.MVC.Controllers
{
    public class StronglyTypedDataController : Controller
    {
        List<FamilyMemberViewModel> familyList = new List<FamilyMemberViewModel>()
        {
            new FamilyMemberViewModel(){Name="Shawn", Relation= "Self",
                Birthday = new DateTime(1999, 1, 15)},
            new FamilyMemberViewModel(){Name="Bob", Relation= "Dad",
                Birthday = new DateTime(1901, 1, 15)},
            new FamilyMemberViewModel(){Name="Mary", Relation= "Sister",
                Birthday = new DateTime(1999, 1, 15)}
        };
        // GET: StronglyTypedData
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SingleFamilyMember()
        {
            FamilyMemberViewModel fm = new FamilyMemberViewModel()
            {
                Name = "Billy",
                Relation = "Cousin",
                Birthday = new DateTime(2021, 1, 15)
            };
            ViewBag.Hello = "This is the ViewBag from the controller";
            //  Pass the object back to the strongly typed view as 
            // part of the view's parameter set
            return View(fm);
        }

        public ActionResult All()
        {
            return View(familyList.ToList());
        }

        //Process the Create Form Data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FamilyMemberViewModel fmvm)
        {
            familyList.Add(fmvm);
            return View("All", familyList);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            // It is best practice to confirm the "state" of the model
            if (!ModelState.IsValid)
            {
                //  Send them back to the form, by passing the object to the view, 
                //the form returns with the original populated information.
                return View(cvm);
            }

            // Below code only executes if the form (object) passes model validation
            string returnMessage = $"You have received an email from {cvm.Name} with a subject " +                $"{cvm.Subject}.  Please respond to {cvm.Email} with your response to " +                $"the following message: <br />{cvm.Message}";

            Boolean isMailSetUp = true;

            if (isMailSetUp)
            {
                //Add using statements for the System Mail
                //Mailmessage Package is what sends the email (System.Net.Mail)
                MailMessage mm = new MailMessage(
                    // From
                     ConfigurationManager.AppSettings["EmailUser"].ToString(),//"postmaster@ryanjpalmer.net",
                                                  // To
                    ConfigurationManager.AppSettings["EmailTo"].ToString(),//"admin@ryanjpalmer.net",
                    cvm.Subject, returnMessage
                    );

                //Mailmessage properties
                //Allow HTML formatting
                mm.IsBodyHtml = true;

                //Set Mail priority
                mm.Priority = MailPriority.High; //Default is normal priority

                //Set up the reply list
                mm.ReplyToList.Add(cvm.Email);

                //  SmtpClient - This is the information from the HOST (smarterAsp.net) 
                // that allows the email to actually be sent
                SmtpClient client = new SmtpClient(
                    ConfigurationManager.AppSettings["EmailClient"].ToString());//"mail.ryanjpalmer.net");

                //  Client credentials (smarterASP requires your user name and password)
                client.Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["EmailUser"].ToString(),//"postmaster@ryanjpalmer.net",
                    ConfigurationManager.AppSettings["EmailPassword"].ToString());//"KPcp5540$");

                //  It is possible that the mailserver is down or we may have configuration 
                // issues, so we want to encapsulate our code in a try/catch
                try
                {
                    //Attempt to send the email
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    ViewBag.CustomerMessage = $"We're sorry your request could not be " +
                        $"completed at this time." +                        $"  Please try again later.  Error Message: <br /> {ex.StackTrace}";
                    return View(cvm); //  Return the view with the entire message so that 
                                      //  users can copy/paste it for later

                    
                }

            }

            return View("EmailConfirmation", cvm); //Upon successful email send users to
                                                   //a friendly confirmation page

        }
    }
}