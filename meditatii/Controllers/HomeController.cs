using meditatii.Models;
using meditatii.web.Filters;
using meditatii.web.Utils;
using Meditatii.Core;
using Meditatii.Core.Helpers;
using Microsoft.AspNet.Identity;
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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Meditatii.Core.Entities;

namespace meditatii.Controllers
{
    public class HomeController : BaseController
    {
        private ApplicationUserManager _userManager;
        public ICategoryService categoryService;

        public HomeController(IUsersService usersService, ICategoryService categoryService)
        {
            this.usersService = usersService;
            this.categoryService = categoryService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
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
        public ActionResult Category(string category)
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
            ViewBag.TotalUser = AppStats.Current.TotalUser;
            ViewBag.TotalMeetingMinutes = AppStats.Current.TotalMeetingMinutes;

            Category cat = categoryService.GetCategoryByName(category);
            if (cat != null)
            {
                ViewBag.CategoryId = cat.Id;
                ViewBag.CategoryName = cat.Name;                
            }

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

        [ActionName("sendnewsletter")]
        public async Task<ActionResult> SendNewsletter()
        {
            var dir = Server.MapPath("~\\newsletterlogs");
            var file = Path.Combine(dir, "newsletterlogs.txt");
            string emailaddresslog = "";
            string newsletterTemplate = "";

            try
            {

                string newsletterItemTemplate = EmailHelper.GetEmailTemplate("newsletter-item");
                string newsletterItem, teacherlink, teachername, teacherprofileimage, teacherteaching;
                string teachersList = "";

                int i = 1;

                SearchResult<User> teachers = usersService.GetTeachersForNewsletter(516);
                
                foreach (var teacher in teachers.Entities)
                {
                    newsletterItem = newsletterItemTemplate;
                    teacherlink = ConfigurationManager.AppSettings["WebSite.URL"] + "/teacherprofile/" + Security.EncryptID(teacher.Id);
                    teachername = teacher.FirstName + " " + (!String.IsNullOrEmpty(teacher.LastName) ? teacher.LastName.Substring(0, 1) + "." : "");
                    teacherprofileimage = ConfigurationManager.AppSettings["WebSite.URL"] + teacher.ProfileImageUrl;

                    teacherteaching = "";
                    if (teacher.Categories.Count > 0)
                    {
                        Category subcategory = teacher.Categories.FirstOrDefault(x => x.ParentId != 0);
                        if (subcategory != null)
                        {
                            teacherteaching = subcategory.Name;
                        }
                        else
                        {
                            teacherteaching = teacher.Categories.FirstOrDefault().Name;
                        }
                    }
                    
                    newsletterItem = newsletterItem.Replace("{teacherlink}", teacherlink);
                    newsletterItem = newsletterItem.Replace("{teachername}", teachername);
                    newsletterItem = newsletterItem.Replace("{teacherprofileimage}", teacherprofileimage);
                    newsletterItem = newsletterItem.Replace("{teacherteaching}", teacherteaching);
                    //construct email

                    teachersList = teachersList + newsletterItem;
                    if (i % 2 == 0)
                    {
                        teachersList = teachersList + "<td width=\"15\"></td>";
                        teachersList = teachersList + "</tr>";
                        teachersList = teachersList + "<tr>";
                        teachersList = teachersList + "<td width=\"480\" colspan=\"5\" height=\"15\"></td>";
                        teachersList = teachersList + "</tr>";
                        teachersList = teachersList + "<tr>";
                    }
                    i++;
                }

                newsletterTemplate = EmailHelper.GetEmailTemplate("newsletter");
                newsletterTemplate = newsletterTemplate.Replace("{teacherslist}", teachersList);

                //get list of users to send
                SearchResult<User> students = usersService.GetStudentsForNewsletter(null);
                foreach (var student in students.Entities)
                {
                    //send email
                    if (student.Email == "elev@gmail.com")
                        continue;

                    await UserManager.EmailService.SendAsync(
                            new IdentityMessage
                            {
                                Body = newsletterTemplate,
                            //Destination = ConfigurationManager.AppSettings["SendEmail.Admin"],
                            Destination = student.Email,
                                Subject = "Buna " + student.LastName + " au apărut noi meditatori în zona ta"
                            });
                            
                    emailaddresslog = emailaddresslog + student.Email + ",";

                }

                string[] loglines = { "--------------------------------------------" + DateTime.Now,
                                    "Start Sending Newsletter:" + DateTime.Now.ToString(),
                                    "--------------------------------------------",
                                    "Email:" + newsletterTemplate,
                                    "--------------------------------------------",
                                    "Students:" + students.TotalRows,
                                    emailaddresslog,
                                    "END--------------------------------------------" };


                System.IO.File.AppendAllLines(file, loglines);

            }
            catch (Exception ex)
            {
                string[] loglines = { "--------------------------------------------"  + DateTime.Now,
                                    newsletterTemplate,
                                    emailaddresslog,
                                    ex.Message,
                                    ex.StackTrace,
                                    "END--------------------------------------------" };
                System.IO.File.AppendAllLines(file, loglines);
            }

            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactViewModel model)
        {
            ViewBag.user = CurrentUser;
            if (model.Operation != "emeditatii") ///for spamers
            {
                return View(model);
            }
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