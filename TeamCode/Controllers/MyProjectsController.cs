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
           // var datacontext = new ApplicationDbContext();
            string userId = User.Identity.GetUserId();
            var projects = _db.Projects.Where(t => t.user.Id == userId);
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
            Project proj = _db.Projects.Find(id);

            if (proj == null)
            {
                return HttpNotFound();
            }

            return View(proj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,projectName,user")] Project proj)
        {

            if(ModelState.IsValid)
            {
                //List<UserToProject> up;
                _db.Entry(proj).State = EntityState.Modified;
                /*up =_db.UsersToProjects.Where(u2p => u2p.project.id.Equals(proj.id)).ToList(); 
                foreach(UserToProject temp in up )
                {
                    _db.UsersToProjects.Remove(temp);
                    temp.project = proj;
                    _db.UsersToProjects.Add(temp);
                    _db.Entry(temp).State = EntityState.Modified;
                }*/
                
                _db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(proj);
        }
    }
}