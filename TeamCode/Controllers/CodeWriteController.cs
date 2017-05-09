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
        public ActionResult Index(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ViewBag.Code = "alert('Hello world!');";
            ViewBag.Code = FileService.Instance.GetValueFromContent(id.Value);
            ViewBag.documentID = id.Value;
            ViewBag.fileType = FileService.Instance.GetFileType(id.Value);
            ViewBag.fileName = FileService.Instance.GetFileName(id.Value);
            ViewBag.projectID = FileService.Instance.GetFileProjectID(id.Value).id;
            ViewBag.userID = FileService.Instance.GetFileUserID(id.Value).Id;

            return View();
        }

        // GET: CodeWrite/Edit/5
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
            return View("CodeWrite");
        }
    }
}
