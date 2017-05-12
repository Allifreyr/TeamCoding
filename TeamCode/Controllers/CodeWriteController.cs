using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamCode.Models;
using TeamCode.Models.Entities;
using TeamCode.Services;

namespace TeamCode.Controllers
{
    public class CodeWriteController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();



        // GET: CodeWrite
        [Authorize]
        public ActionResult Index(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            File file = _db.Files.Find(id);
            int projectId = file.project.id;
            List<UserToProjects> up = UserToProjectsService.Instance.GetUserWithProjectID(projectId);
            bool userFound = false;
            //Check if logged in user is owner or member of project
            try
            {

                for (int i = 0; i < up.Count; i++)
                {
                    if(up[i].user.Id == Session["userId"].ToString())
                    {
                        userFound = true;
                    }
                }

                if (file.user.Id == Session["userId"].ToString())
                {
                    userFound = true;
                }

                    if (!userFound)
                {
                    return RedirectToAction("Index", "MyProjects"); //Redirect to Myproject
                }

            }
            catch
            {
                if(Request.IsAuthenticated)
                {
                    return RedirectToAction("Index", "MyProjects");
                }
                return RedirectToAction("Index", "Home");
            }


            ViewBag.Code = _db.Files.Where(gvc => gvc.id == id).SingleOrDefault().content;
            ViewBag.documentID = id.Value;
            //ViewBag.fileType = FileService.Instance.GetFileType(id.Value);
            ViewBag.fileType = _db.Files.Where(gft => gft.id == id).SingleOrDefault().fileType;
            //ViewBag.fileName = FileService.Instance.GetFileName(id.Value);
            ViewBag.fileName = _db.Files.Where(gfn => gfn.id == id).SingleOrDefault().fileName;
            ViewBag.projectID = FileService.Instance.GetFileProjectID(id.Value).id;
            //ViewBag.projectID = _db.Files.Where(gfp => gfp.id == id).SingleOrDefault().project;
            //ViewBag.userID = FileService.Instance.GetFileUserID(id.Value).Id;
            ViewBag.userID = _db.Files.Find(id).user;


            return View();
        }

        // GET: CodeWrite/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CodeWrite/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        //   [ValidateAntiForgeryToken]
        public ActionResult SaveCode([Bind(Include = "id,fileName,content,fileType,projectid,userid")] FileViewModel file)
        {
            if(ModelState.IsValid)
            {
                File f = new File
                {
                    id = file.id,
                    content = file.content,
                    fileName = file.fileName,
                    fileType = file.fileType,
                    project = (from p in _db.Projects
                               where p.id == file.projectid
                               select p).SingleOrDefault(),
                    user = (from u in _db.Users
                            where u.Id == file.userid
                            select u).SingleOrDefault()
                };

                _db.Entry(f).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("Index", "CodeWrite", new { id = file.id });
            }
            return View("Error");
        }
    }
}
