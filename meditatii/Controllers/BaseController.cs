using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Enums;
using Meditatii.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace meditatii.Controllers
{
    public class BaseController: Controller
    {
        public IUsersService usersService;

        public UserModel CurrentUser
        {
            get
            {
                if (HttpContext == null)
                    return null;
                if (HttpContext.User == null)
                    return null;
                if (HttpContext.User.Identity.Name == null || HttpContext.User.Identity.Name == "")
                    return null;

                if (Request.QueryString["refreshuser"] == null)
                {
                    if (Session["CurrentUser"] != null)
                    {
                        return (UserModel)Session["CurrentUser"];
                    }
                }

                UserModel currentuser = MappingHelper.Map<UserModel>(this.usersService.GetUser(HttpContext.User.Identity.Name));
                currentuser.IsTeacher = HttpContext.User.IsInRole(UserType.Teacher.ToString());
                
                Session["CurrentUser"] = currentuser;

                return (UserModel)Session["CurrentUser"];
            }

            set { Session["CurrentUser"] = value; }
        }

    }
}