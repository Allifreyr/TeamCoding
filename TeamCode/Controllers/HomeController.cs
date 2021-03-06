﻿using System.Web.Mvc;

namespace TeamCode.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Request.IsAuthenticated)                         //If user is logged in
            {
                return RedirectToAction("Index", "MyProjects"); //Redirect to Myproject
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Support()
        {
            ViewBag.Message = "Here you can view frequently asked questions";

            return View();
        }
    }
}