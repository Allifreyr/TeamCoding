﻿using System.Data;
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

            /*  var users = from a in db.UsersToProjects
                          select a;
              if(!string.IsNullOrEmpty(searchString))
              {
                  users = users.Where(s => s.user.UserName.Contains(searchString));
              }*/
            //return View(users);

            //    int projectId = ProjectService.Instance.GetProjectByID(id.Value).id;
            //   Session["projectId"] = id.Value;
            /*     var up = _db.UsersToProjects.Where(p => p.project.id == id.Value);

                 UserToProjectsViewModel upvm = new UserToProjectsViewModel
                 {
                     ide = up.id,
                     projectId = up.project.id,
                     userId = null
                 };
                 return View(up);*/
            //  return View();

            //  var p = ProjectService.Instance.GetProjectByID(id.Value);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ProjectName = ProjectService.Instance.GetProjectByID(id.Value).projectName;
            ViewBag.ProjectOwner = ProjectService.Instance.GetProjectByID(id.Value).user.Email;

            var u = _db.UsersToProjects.Where(up => up.project.id == id).ToList();

            if (u == null)
            {
                return View();
            }

            return View(u);
        }

        // GET: UserToProjects/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserToProjects userToProject = _db.UsersToProjects.Find(id);
            if(userToProject == null)
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

            UserToProjects up = new UserToProjects
            {
                project = (from p in _db.Projects
                           where p.id == id.Value
                           select p).SingleOrDefault(),
                user = null
            };

            UserToProjectsViewModel upvm = new UserToProjectsViewModel
            {
                ide = up.id,
                projectId = up.project.id,
                userId = null
            };

            return View(upvm);
        }

        // POST: UserToProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ide,userId,projectId")] UserToProjectsViewModel userToProject)
        {
            if(ModelState.IsValid)
            {
                /*var checkProject = (from p in _db.UsersToProjects
                                    where p.project.id == userToProject.projectId
                                    select p)*/
                try
                {
                    var emailId = _db.Users.Where(bla => bla.Email == userToProject.userId).SingleOrDefault();
                    UserToProjects up = new UserToProjects
                    {
                        id = userToProject.ide,
                        project = (from p in _db.Projects
                                   where p.id == userToProject.projectId
                                   select p).SingleOrDefault(),
                        user = emailId
                    };
                    _db.UsersToProjects.Add(up);
                    _db.SaveChanges();

                    return RedirectToAction("Index", new { id = up.project.id });
                }
                catch
                {
                    return View("Error");
                }

                
            }

            return View(userToProject);
        }

        // GET: UserToProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserToProjects userToProject = _db.UsersToProjects.Find(id);
            if(userToProject == null)
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
            if(ModelState.IsValid)
            {
                _db.Entry(userToProject).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userToProject);
        }

        // GET: UserToProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserToProjects userToProject = _db.UsersToProjects.Find(id);
            if(userToProject == null)
            {
                return HttpNotFound();
            }
            return View(userToProject);
        }

        // POST: UserToProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserToProjects userToProject = _db.UsersToProjects.Find(id);
            _db.UsersToProjects.Remove(userToProject);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
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
            if(ModelState.IsValid)
            {
                //TODO: SubscribeUser(model.Email);
            }

            return View("Index", model);
        }
    }
}
