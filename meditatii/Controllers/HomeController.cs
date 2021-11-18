using meditatii.Models;
using meditatii.web.Filters;
using meditatii.web.Utils;
using Meditatii.Core;
using Meditatii.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace meditatii.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [AllowedIP]
        public ActionResult Index()
        {
            ViewBag.user = CurrentUser;
            if (AppStats.Current.TotalTeacher == 0)
            {
                UserSatsModel userSatsModel = MappingHelper.Map<UserSatsModel>(usersService.GetUserStats());

                //update current stats
                if (userSatsModel != null)
                {
                    AppStats.Current.TotalTeacher = userSatsModel.TotalTeacher;
                    AppStats.Current.TotalUser = userSatsModel.TotalUser;
                    AppStats.Current.TotalMeetingMinutes = userSatsModel.TotalMeetingMinutes;
                }
            }
            ViewBag.TotalTeacher = AppStats.Current.TotalTeacher;
            ViewBag.TotalUser= AppStats.Current.TotalUser;
            ViewBag.TotalMeetingMinutes = AppStats.Current.TotalMeetingMinutes;

            return View();
        }

        [AllowedIP]
        public ActionResult About()
        {
            ViewBag.user = CurrentUser;
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowedIP]
        public ActionResult Contact()
        {
            ViewBag.user = CurrentUser;
            ViewBag.Message = "";

            return View();
        }

        [AllowedIP]
        public ActionResult CumFunctioneaza()
        {
            ViewBag.user = CurrentUser;
            ViewBag.Message = "Cum functioneaza - in curand.";

            return View();
        }

        [AllowedIP]
        [ActionName("termeni-si-conditii")]
        public ActionResult termenisiconditii()
        {
            ViewBag.user = CurrentUser;
            ViewBag.Message = "Termeni si conditii";

            return View();
        }

        [AllowedIP]
        [ActionName("politica-de-confidentialitate")]
        public ActionResult confidentialitate()
        {
            ViewBag.user = CurrentUser;
            ViewBag.Message = "Politica de confidentialitate";

            return View();

        }

        [ActionName("workinprogress")]
        public ActionResult workinprogress()
        { 
            return View();
        }


        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactViewModel model)
        {
            ViewBag.user = CurrentUser;
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