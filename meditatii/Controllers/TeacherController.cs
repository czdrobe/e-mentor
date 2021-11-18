using meditatii.web.Filters;
using Meditatii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace meditatii.Controllers
{
    public class TeacherController : BaseController
    {
        public TeacherController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [AllowedIP]
        public ActionResult Index()
        {
            ViewBag.user = CurrentUser;
            return View();
        }

    }
}