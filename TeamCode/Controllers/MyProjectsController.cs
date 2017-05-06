﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamCode.Services;
using TeamCode.Models.Entities;

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