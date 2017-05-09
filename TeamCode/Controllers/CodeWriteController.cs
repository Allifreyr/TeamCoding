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
        //[ValidateAntiForgeryToken]
        public ActionResult SaveCode([Bind(Include = "id,fileName,content,fileType,project,user")] File file)
        {
            if(ModelState.IsValid)
            {
                _db.Entry(file).State = EntityState.Modified;
                _db.SaveChanges();
                //return RedirectToAction("CodeWrite");
                return RedirectToAction("Index", "CodeWrite", new { id = file.id });
            }
            return View("CodeWrite");
        }
    }
}
