using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamCode.Controllers
{
    public class CodeWritingController : Controller
    {
        // GET: CodeWriting
        public ActionResult Index()
        {
            return View();
        }

        // GET: CodeWriting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CodeWriting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CodeWriting/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CodeWriting/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CodeWriting/Edit/5
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

        // GET: CodeWriting/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CodeWriting/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
