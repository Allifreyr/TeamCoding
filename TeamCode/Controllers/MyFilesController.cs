using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamCode.Services;
using TeamCode.Models.Entities;
using Microsoft.AspNet.Identity;

namespace TeamCode.Controllers
{
    public class MyFilesController : Controller
    {
        
        // GET: MyFiles
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateFile()
        {
            string userId = User.Identity.GetUserId();
            FileService.Instance.AddNewFile(userId);
            return RedirectToAction("Index", "Myfiles");
        }
    }
}