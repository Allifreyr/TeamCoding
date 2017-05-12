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

            ViewBag.ProjectName = ProjectService.Instance.GetProjectByID(id.Value).projectName;
            ViewBag.ProjectOwner = ProjectService.Instance.GetProjectByID(id.Value).user.Email;
            ViewBag.ProjectID = id.Value;

            var f = FileService.Instance.GetFilesByProject(id);

            if(f == null)
            {
                return View();
            }

            return View(f);
        }

        public ActionResult CreateFile(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int projectId = id.Value;
            string userId = User.Identity.GetUserId();

            //Check names
            FileService.Instance.AddNewFile(userId, projectId);
            return RedirectToAction("Index", "Myfiles", new { id = projectId });

        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = FileService.Instance.GetFileByID(id.Value);
            if(file == null)
            {
                return HttpNotFound();
            }
            else if(file.user != null)
            {
                string thisFileUserId = null;
                thisFileUserId = file.user.Id;
                if(thisFileUserId != Session["userId"].ToString())  //Check if user id for this project is yours
                {
                    return View("Error");                            //Project doesn't belong to you
                }
                return View(file);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult Edit(File file)
        {
            Project proj = FileService.Instance.GetFileProjectID(file.id);
            File edit = FileService.Instance.PostFileByID(file);
            File returnById = FileService.Instance.GetFileByID(file.id);

            if(edit != null)
            {
                return RedirectToAction("Index", new { id = edit.project.id });
            }
            else
            {
                return RedirectToAction("Edit", "Myfiles", new { id = returnById.id });
            }
        }

        public ActionResult DeleteFile(int? id)
        {
            if(ModelState.IsValid)
            {
                int returnId = FileService.Instance.DeleteFile(id);
                return RedirectToAction("Index", "Myfiles", new { id = returnId });
            }
            return View("Error");
        }
    }
}