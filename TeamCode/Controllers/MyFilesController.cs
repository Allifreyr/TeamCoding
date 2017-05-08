using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamCode.Services;
using TeamCode.Models.Entities;
using Microsoft.AspNet.Identity;
using System.Net;
using TeamCode.Models;
using System.Data.Entity;

namespace TeamCode.Controllers
{
    public class MyFilesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: MyFiles
        [Authorize]
        public ActionResult Index(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var f = FileService.Instance.GetFilesByProject(id.Value);

            var p = ProjectService.Instance.GetProjectByID(id.Value);
            ViewBag.ProjectName = p.projectName;
            ViewBag.ProjectOwner = p.user.UserName;

            ViewBag.ProjectID = id.Value;

            return View(f);
        }

        public ActionResult CreateFile(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int projectId = id.Value;
            string userId = User.Identity.GetUserId();
            FileService.Instance.AddNewFile(userId, projectId);
            return RedirectToAction("Index", "Myfiles", new { id = projectId });

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = FileService.Instance.GetFileByID(id.Value);
            if (file == null)
            {
                return HttpNotFound();
            }

            return View(file);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fileName,content,fileType,project,user")] File file)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(file).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }
    }
}