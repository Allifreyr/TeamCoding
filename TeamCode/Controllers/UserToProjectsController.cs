using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TeamCode.Models;
using TeamCode.Models.Entities;
using TeamCode.Models.ViewModels;
using TeamCode.Services;

namespace TeamCode.Controllers
{
    public class UserToProjectsController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: UserToProjects
        //public ActionResult Index(string searchString)
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ProjectName = ProjectService.Instance.GetProjectByID(id.Value).projectName;
            ViewBag.ProjectOwner = ProjectService.Instance.GetProjectByID(id.Value).user.Email;

            var utpIndex = UserToProjectsService.Instance.GetProjectById(id);

            if (utpIndex == null)
            {
                return View();
            }

            return View(utpIndex);
        }

        // GET: UserToProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userToProject = UserToProjectsService.Instance.FindUserWithProjectID(id);
            if (userToProject == null)
            {
                return HttpNotFound();
            }
            return View(userToProject);
        }

        // GET: UserToProjects/Create
        public ActionResult Create(int? id)
        {
            ViewBag.ProjectName = ProjectService.Instance.GetProjectByID(id.Value);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserToProjects up = UserToProjectsService.Instance.NewUserToProject(id);

            UserToProjectsViewModel upvm = UserToProjectsService.Instance.NewUserToProjectViewModel(up);

            return View(upvm);
        }

        // POST: UserToProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ide,userId,projectId")] UserToProjectsViewModel userToProject)
        {
            if (ModelState.IsValid)
            {
                //Check if user already has access to project
                var uit = UserToProjectsService.Instance.userInTable(userToProject);
                var ue = UserToProjectsService.Instance.userExists(userToProject);
                var project = ProjectService.Instance.GetProjectByID(userToProject.projectId);

                if (project == null || project.user.Id == userToProject.userId)
                {
                    return View("Create");
                }

                if (ue.Count == 0)
                {
                    ModelState.AddModelError("Email", "This Email doesn't exist. Please check the spelling");
                    return View("Create");
                }

                if (uit != null)
                {
                    for (int i = 0; i < uit.Count; i++)
                    {
                        if (uit[i].project.id == userToProject.projectId)
                        {
                            //If user already exists in project then this error messages appears.
                            ModelState.AddModelError("Email", "Email address already exists for this project. Please enter a different email address.");
                            return View("Create");
                        }
                    }
                }

                try
                {
                    var up = UserToProjectsService.Instance.UserToProjectList(userToProject);

                    return RedirectToAction("Index", new { id = up.project.id });
                }
                catch
                {
                    return View("Error");
                }


            }

            return View(userToProject);
        }

        private ActionResult View(object v, UserToProjectsViewModel userToProject)
        {
            throw new NotImplementedException();
        }

        // GET: UserToProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserToProjects userToProject = UserToProjectsService.Instance.FindUserWithProjectID(id);
            if (userToProject == null)
            {
                return HttpNotFound();
            }
            return View(userToProject);
        }

        // POST: UserToProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id")] UserToProjects userToProject)
        {
            if (ModelState.IsValid)
            {
                UserToProjectsService.Instance.SaveUserToProject(userToProject);
                return RedirectToAction("Index");
            }
            return View(userToProject);
        }

        // GET: UserToProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectId = UserToProjectsService.Instance.FindUserWithProjectID(id).project.id;

            if (ModelState.IsValid)
            {
                UserToProjectsService.Instance.DeleteUserToProject(id);
                return RedirectToAction("Index", "UserToProjects", new { id = projectId });
            }
            return View("Error");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult addUserToProject(string userId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult addUserToProject(UserToProjectsViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: SubscribeUser(model.Email);
            }

            return View("Index", model);
        }
    }
}
