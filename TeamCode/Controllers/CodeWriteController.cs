using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamCode.Models;

namespace TeamCode.Controllers
{
    public class CodeWriteController : Controller
    {
        // GET: CodeWrite
        public ActionResult Index()
        {
            ViewBag.Code = "alert('Hello world!');";
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
        public ActionResult SaveCode(CodeWriteViewModel model)
        { 
            return View("CodeWrite");
        }
    }
}
