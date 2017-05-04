 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamCode.Controllers
{

    // Kata að bulla til að geta pushað og pullað :)
    // alexandra að bulla til að geta pushað og pullað :)
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Support()
        {
            ViewBag.Message = "Your support page.";

            return View();
        }
    }
}