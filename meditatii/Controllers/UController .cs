using meditatii.Models;
using meditatii.web.Filters;
using Meditatii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace meditatii.Controllers
{
    public class UController : BaseController
    {
        public UController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [AllowedIP]
        public ActionResult Index()
        {
            ViewBag.user = CurrentUser;
            ViewBag.CssExtra = "weapper-helper";
            return View( new HomeViewModel() { CssExtra  = "weapper-helper" });
        }
    }
}