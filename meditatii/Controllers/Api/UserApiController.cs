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
        public SearchResult<UserModel> GetUsers(int? categoryid, int? cycleid, int page)
        {
            int itemsPerPage = 10;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;
            return MappingHelper.Map<SearchResult<UserModel>>(this.usersService.GetUsers(categoryid, cycleid, skip, take));
        }

        [HttpGet]
        [Route("api/users/getuser")]
        public UserModel GetUser(int userid)
        {
            return MappingHelper.Map<UserModel>(this.usersService.GetUser(userid));
        }

        [HttpGet]
        [Route("api/users/getcurrentprofile")]
        public UserModel GetCurrentProfile()
        {
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
        }

        [HttpGet]
        [Route("api/users/getavailability")]
        public SearchResult<TeacherAvailabilityModel> GetAvailability()
        {
            return MappingHelper.Map<SearchResult<TeacherAvailabilityModel>>(this.teacherAvailabilityService.GetAvailability(HttpContext.Current.User.Identity.Name,0,100));
        }

        [HttpGet]
        [Route("api/users/getavailabilityforday")]
        public SearchResult<TeacherAvailabilityModel> GetAvailabilityForDay(int userId, DateTime day)
        {
            return MappingHelper.Map<SearchResult<TeacherAvailabilityModel>>(this.teacherAvailabilityService.GetAvailabilityTeacherAvaiabilityForDay(userId, day));
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
        public void SaveAppoitment(int teacherId, string selectedDate, int startTime)
        {
            string[] arrDate = selectedDate.Split('-');
            if (arrDate.Length != 3)
                return;

            DateTime startDate = new DateTime(Convert.ToInt32(arrDate[0]),Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[2]),startTime,0,0);
            DateTime endDate = startDate.AddHours(1);//TODO: maybe not good idea to hardcord to 1 hour

            this.appoitmentService.SaveNewAppoitment(HttpContext.Current.User.Identity.Name, teacherId, startDate, endDate);
        }
    }
}