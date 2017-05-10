using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamCode.Models;
using TeamCode.Models.Entities;
using TeamCode.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace TeamCode.Controllers
{
    public class UserToProjectsController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: UserToProjects
        //public ActionResult Index(string searchString)
        public ActionResult Index()
        {

          /*  var users = from a in db.UsersToProjects
                        select a;
            if(!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.user.UserName.Contains(searchString));
            }*/
            //return View(users);
            return View();
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
            _db.UsersToProjects.Add(up);
            _db.SaveChanges();

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
                try
                {
                    var emailId = _db.Users.Where(bla => bla.Email == userToProject.userId).SingleOrDefault().Id;
                }
                catch
                {
                    return View("Error");
                }
                
                UserToProjects up = new UserToProjects
                {
                    id = userToProject.ide,
                    project = (from p in _db.Projects
                               where p.id == userToProject.projectId
                               select p).SingleOrDefault(),
                    user = _db.Users.Where(bla => bla.Email == userToProject.userId).SingleOrDefault()
            };

                _db.Entry(up).State = EntityState.Modified;
                _db.SaveChanges();

                return View("Index");
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
