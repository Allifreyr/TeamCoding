using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamCode.Services;
using TeamCode.Models.Entities;
using Microsoft.AspNet.Identity;
using TeamCode.Models;

namespace TeamCode.Controllers
{
    public class MyProjectsController : Controller
    {
        
        // GET: Project
        [Authorize]
        public ActionResult Index()
        {
            var datacontext = new ApplicationDbContext();
            string userId = User.Identity.GetUserId();
            var projects = ProjectService.Instance.GetProjectsByUser(userId);
            return View(projects);
            
        }

        public ActionResult CreateProject()
        {
            string userId = User.Identity.GetUserId();
            int projectId = ProjectService.Instance.AddNewProject(userId);
            FileService.Instance.AddNewFile(userId, projectId, "Index", ".cs");
            return RedirectToAction("Index", "MyProjects");
        }
    }
}