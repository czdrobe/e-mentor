using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using Meditatii.Core.Enums;
using System.Collections.Generic;
using System.IO;
using System;
using meditatii.web.Utils;
using System.Web.Hosting;
using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using Meditatii.CoreNew.Enums;

namespace meditatii.Controllers.Api
{
    public class UserApiController : ApiController
    {
        private IAppoitmentService appoitmentService;
        private IUsersService usersService;
        private ITeacherAvailabilityService teacherAvailabilityService;
        private ICategoryService categoryService;
        private ICycleService cycleService;
        private ApplicationUserManager _userManager;

        public UserApiController(IUsersService usersService, ITeacherAvailabilityService teacherAvailabilityService, ICategoryService categoryService, ICycleService cycleService, IAppoitmentService appoitmentService)
        {
            this.usersService = usersService;
            this.teacherAvailabilityService = teacherAvailabilityService;
            this.categoryService = categoryService;
            this.cycleService = cycleService;
            this.appoitmentService = appoitmentService;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set
            {
                _userManager = value;
            }
        }


        [HttpGet]
        [Route("api/users/getusers")]
        public SearchResult<UserModel> GetUsers(int? categoryid, int? cycleid, int? cityId, int? order, int page)
        {
            int itemsPerPage = 10;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;
            return MappingHelper.Map<SearchResult<UserModel>>(this.usersService.GetUsers(categoryid, cycleid, cityId, order, skip, take));
        }

        [HttpGet]
        [Route("api/users/GetSuggestedUsers")]
        public SearchResult<UserModel> GetSuggestedUsers(int? categoryid, int? cityId)
        {
            return MappingHelper.Map<SearchResult<UserModel>>(this.usersService.GetSuggestedUsers(HttpContext.Current.User.Identity.Name, categoryid, cityId));
        }

        [HttpGet]
        [Route("api/users/GetRequests")]
        public SearchResult<RequestModel> GetRequests(string city, string subject, int page)
        {
            int itemsPerPage = 10;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;

            return MappingHelper.Map<SearchResult<RequestModel>>(this.usersService.GetRequests(city, subject, skip, take));
        }

        [HttpGet]
        [Route("api/users/GetRequest")]
        public RequestModel GetRequest(string id)
        {
            return MappingHelper.Map<RequestModel>(this.usersService.GetRequest(Security.DecryptID(id)));
        }

        [HttpPost]
        [Route("api/users/SaveNewRequest")]
        public string SaveNewRequest(RequestModel newRequest)
        {
            var currentprofile = this.usersService.GetUser(HttpContext.Current.User.Identity.Name);

            var r = MappingHelper.Map<Request>(newRequest);
            r.Active = true;
            r.StartDate = DateTime.Now;
            r.EndDate = DateTime.Now.AddDays(newRequest.Duration);
            r.LearnerId = currentprofile.Id;
            if (r.CityId == 0)
            {
                r.CityId = null;
            }

            return Security.EncryptID(this.usersService.SaveNewRequest(r));

        }

        [HttpGet]
        [Route("api/users/GetCities")]
        public List<Models.CityModel> GetCities()
        {
            return MappingHelper.Map<List<CityModel>>(this.usersService.GetCities());
        }

        [HttpGet]
        [Route("api/users/getuser")]    
        public UserModel GetUser(int userid)
        {
            return MappingHelper.Map<UserModel>(this.usersService.GetUser(userid));
        }

        [HttpGet]
        [Route("api/users/getuserbycode")]
        public UserModel GetUserByCode(string userid)
        {
            return MappingHelper.Map<UserModel>(this.usersService.GetUser(Security.DecryptID(userid)));
        }

        [HttpGet]
        [Route("api/users/getuserphonenumber")]
        public string GetUserPhoneNumber(string userid)
        {
            this.usersService.IncrementPhoneViews(Security.DecryptID(userid));
            return this.usersService.GetUser(Security.DecryptID(userid)).PhoneNumber;
        }

        [HttpGet]
        [Route("api/users/getcurrentprofile")]
        public UserModel GetCurrentProfile()
        {
            if (HttpContext.Current.User.Identity.Name == "")
                return null;

            var currentprofile = MappingHelper.Map<UserModel>(this.usersService.GetUser(HttpContext.Current.User.Identity.Name));

            if (currentprofile != null)
                currentprofile.IsTeacher = HttpContext.Current.User.IsInRole(UserType.Teacher.ToString());

            return currentprofile;
        }

        [HttpPost]
        [Route("api/users/SaveCurrentProfile")]
        public void SaveCurrentProfile(UserModel user)
        {
            this.usersService.SaveUser(MappingHelper.Map<User>(user));

            //force to update the user in session
            //HttpContext.Current.Session["CurrentUser"] = null;
        }

        [HttpGet]
        [Route("api/users/getavailability")]
        public SearchResult<TeacherAvailabilityModel> GetAvailability()
        {
            return MappingHelper.Map<SearchResult<TeacherAvailabilityModel>>(this.teacherAvailabilityService.GetAvailability(HttpContext.Current.User.Identity.Name,0,100));
        }

        [HttpGet]
        [Route("api/users/getavailabilityforday")]
        public SearchResult<TeacherAvailabilityModel> GetAvailabilityForDay(string userId, DateTime day)
        {
            try
            {
                return MappingHelper.Map<SearchResult<TeacherAvailabilityModel>>(this.teacherAvailabilityService.GetAvailabilityTeacherAvaiabilityForDay(Security.DecryptID(userId), day));
            }
            catch (Exception ex)
            {
                var path = HostingEnvironment.MapPath("~") + "notification.log";
                File.AppendAllLines(path, new string[] { ex.Message + ex.StackTrace.ToString() });
            }
            return null;
        }

        [HttpPost]
        [Route("api/users/saveavailability")]
        public void SaveAvailability(List<TeacherAvailabilityModel> lstAvaiability)
        {
            this.teacherAvailabilityService.SaveAvailability(HttpContext.Current.User.Identity.Name, MappingHelper.Map<List<TeacherAvailability>>(lstAvaiability));
        }

        [HttpPost]
        [Route("api/users/savecategories")]
        public void SaveCategories(List<Models.CategoryModel> lstCategories)
        {
            this.categoryService.SaveCategoriesForUser(HttpContext.Current.User.Identity.Name, MappingHelper.Map<List<Category>>(lstCategories));
        }

        [HttpPost]
        [Route("api/users/savecycles")]
        public void SaveCycles(List<Models.CategoryModel> lstCycles)
        {
            this.cycleService.SaveCyclesForUser(HttpContext.Current.User.Identity.Name, MappingHelper.Map<List<Cycle>>(lstCycles));
        }

        [HttpPost]
        [Route("api/users/savecityforcurrentprofie")]
        public void SaveCityForCurrentProfie([FromBody] SaveCityRequest cities)
        {
            this.usersService.SaveCityForUser(HttpContext.Current.User.Identity.Name, cities.cityid1, cities.cityid2, cities.cityid3, cities.isOnline);
        }


        [HttpPost]
        [Route("api/users/saveprofileimage")]
        public void SaveProfileImage()
        {
            if (HttpContext.Current.Request["profileimage"] != null)
            {
                var currentprofile = this.usersService.GetUser(HttpContext.Current.User.Identity.Name);

                string path = HttpContext.Current.Server.MapPath("~/profileimages");
                int indbase = HttpContext.Current.Request["profileimage"].IndexOf("base64");
                string extension = HttpContext.Current.Request["profileimage"].Substring(11, indbase - 12);

                string filenameandpath = path + "/" + currentprofile.Id + "." + extension;
                string filenameandpathrelative = "/profileimages/" + currentprofile.Id + "." + extension;

                File.WriteAllBytes(filenameandpath, Convert.FromBase64String(HttpContext.Current.Request["profileimage"].Substring(indbase + 7)));
                currentprofile.ProfileImageUrl = filenameandpathrelative;
                this.usersService.SaveUser(currentprofile);
            }
        }

        [HttpPost]
        [Route("api/users/saveappoitment")]
        public void SaveAppoitment(string teacherId, string selectedDate, int startTime)
        {
            try
            {
                string[] arrDate = selectedDate.Split('-');
                if (arrDate.Length != 3)
                    return;

                DateTime startDate = new DateTime(Convert.ToInt32(arrDate[0]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[2]), startTime, 0, 0);
                DateTime endDate = startDate.AddHours(1);//TODO: maybe not good idea to hardcord to 1 hour

                int appoitmentId = this.appoitmentService.SaveNewAppoitment(HttpContext.Current.User.Identity.Name, Security.DecryptID(teacherId), startDate, endDate);

                //send email to the teacher
                var userToSendMessage = this.usersService.GetUser(Security.DecryptID((teacherId)));
                var userfromMessage = this.usersService.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
                string urlappoitment = ConfigurationManager.AppSettings["WebSite.URL"] + "/u/appoitments";

                string emailbody = EmailHelper.GetEmailTemplate("newappoitment");
                emailbody = emailbody.Replace("<from-firstname>", userfromMessage.FirstName);
                emailbody = emailbody.Replace("<from-lastname>", userfromMessage.LastName);
                emailbody = emailbody.Replace("<dateandtime>", startDate.ToString());
                emailbody = emailbody.Replace("<appoitmentsurl>", urlappoitment);

                var email = new MailMessage(new MailAddress(ConfigurationManager.AppSettings["SendEmail.Address"], ConfigurationManager.AppSettings["SendEmail.Alias"]),
                            new MailAddress(userToSendMessage.Email))
                {
                    Subject = "Rezervare noua - eMeditatii.ro",
                    Body = emailbody,
                    IsBodyHtml = true
                };

                var client = new SmtpClient();
                client.SendCompleted += (s, e) =>
                {
                    client.Dispose();
                    this.appoitmentService.SetAppoitmentNotification(appoitmentId, AppoitmentNotification.NotificationNew, true);
                };
                Task t = Task.Run(async () =>
                {
                    await client.SendMailAsync(email);
                });
                t.Wait();
            }
            catch (Exception ex)
            {
                var path = HostingEnvironment.MapPath("~") + "notification.log";
                File.AppendAllLines(path, new string[] { ex.Message + ex.StackTrace.ToString() });
            }
        }
    }

    public class SaveCityRequest
    {
        public int cityid1 { get; set; }
        public int cityid2 { get; set; }
        public int cityid3 { get; set; }
        public bool isOnline { get; set; }
    }
}