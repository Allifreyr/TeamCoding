using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamCode.Services;
using TeamCode.Models.Entities;
using Microsoft.AspNet.Identity;
using TeamCode.Models;
using System.Net;
using System.Data.Entity;

namespace TeamCode.Controllers
{
    public class MyProjectsController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Project
        [Authorize]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            Session["userId"] = userId;

            List<Project> projects = ProjectService.Instance.GetProjectsByUser(userId);

            return View(projects);
        }

        [Authorize]
        public ActionResult Shared()
        {
            string userId = User.Identity.GetUserId();
            Session["userId"] = userId;

            List<UserToProjects> upShared = UserToProjectsService.Instance.GetProjectsSharedWithUser(userId);
            List<Project> projects = new List<Project>();

            for(int i = 0; i < upShared.Count; i++)
            {
                projects.Add(ProjectService.Instance.GetProjectByID(upShared[i].project.id));
            }

            return View(projects);
        }

        public ActionResult CreateProject()
        {
            string userId = User.Identity.GetUserId();
            int projectId = ProjectService.Instance.AddNewProject(userId);
            FileService.Instance.AddNewFile(userId, projectId, "Index", ".js");
            return RedirectToAction("Index", "MyProjects");
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ProjectName = ProjectService.Instance.GetProjectByID(id.Value).projectName;

            Project proj = ProjectService.Instance.GetProjectByID(id);
            if(proj == null)
            {
                return HttpNotFound();
            }
            else if(proj.user != null)
            {
                string thisProjUserId = null;
                thisProjUserId = proj.user.Id;
                if(thisProjUserId != Session["userId"].ToString())  //Check if user id for this project is yours
                {
                    return View("Error");                       //Project doesn't belong to you
                }
                return View(proj);
            }
            return View("Error");                               //Uh no, this shouldn't happen
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,projectName,user")] Project proj)
        {

            if(ModelState.IsValid)
            {
                ProjectService.Instance.SaveProject(proj);
                return RedirectToAction("Index");
            }
            return View(proj);
        }

        public ActionResult DeleteProject(int? id)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    ProjectService.Instance.DeleteProject(id);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "You cannot delete a project with users or files attached to them.";
                    return View("Index");
                }
            }
            return View("Error");
        }
    }
}