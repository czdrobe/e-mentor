using meditatii.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace meditatii.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult CumFunctioneaza()
        {
            ViewBag.Message = "Cum functioneaza - in curand.";

            return View();
        }

        public ActionResult termenisiconditii()
        {
            ViewBag.Message = "Termeni si conditii";

            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email =
                   new MailMessage(new MailAddress(ConfigurationManager.AppSettings["SendEmail.Address"]), new MailAddress(ConfigurationManager.AppSettings["SendEmail.Admin"]))
                   {
                       Subject = "eMeditatii.ro - contact form",
                       Body = String.Format("Nume:{0}; Email:{1}; Telefon: {2} Mesaj: {3}", model.Name, model.Email, model.Phone, model.Message),
                       IsBodyHtml = true
                   };

                var client = new SmtpClient();
                client.Send(email);
            }

            ViewBag.MessageSuccess = true;

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}