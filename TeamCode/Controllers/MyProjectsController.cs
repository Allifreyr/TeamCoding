﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamCode.Controllers
{
    public class MyProjectsController : Controller
    {
        // GET: Project
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}