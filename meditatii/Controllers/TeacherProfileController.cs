using meditatii.web.Utils;
using Meditatii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace meditatii.Controllers
{
    public class TeacherProfileController : BaseController
    {

        public TeacherProfileController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public ActionResult Index(string id)
        {
            this.usersService.IncrementVisitorsNumber(Security.DecryptID(id));
            ViewBag.user = CurrentUser;
            return View();
        }

    }
}