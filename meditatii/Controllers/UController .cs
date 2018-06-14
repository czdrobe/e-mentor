﻿using meditatii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace meditatii.Controllers
{
    public class UController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CssExtra = "weapper-helper";
            return View( new HomeViewModel() { CssExtra  = "weapper-helper" });
        }
    }
}