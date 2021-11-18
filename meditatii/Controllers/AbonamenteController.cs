using meditatii.web.Utils;
using Meditatii.Core;
using Meditatii.Core.Entities;
using MobilpayEncryptDecrypt;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace meditatii.Controllers
{
    public class AbonamenteController : BaseController
    {
        public AbonamenteController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public ActionResult Index()
        {
            ViewBag.user = CurrentUser;

            return View();
        }

    }

}