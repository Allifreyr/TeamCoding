﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamCode.Models;
using TeamCode.Models.Entities;

namespace TeamCode.Controllers
{
    public class UserToProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserToProjects
        public ActionResult Index()
        {
            return View(db.UsersToProjects.ToList());
        }

        // GET: UserToProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserToProject userToProject = db.UsersToProjects.Find(id);
            if (userToProject == null)
            {
                return HttpNotFound();
            }
            return View(userToProject);
        }

        // GET: UserToProjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserToProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id")] UserToProject userToProject)
        {
            if (ModelState.IsValid)
            {
                db.UsersToProjects.Add(userToProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userToProject);
        }

        // GET: UserToProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserToProject userToProject = db.UsersToProjects.Find(id);
            if (userToProject == null)
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
        public ActionResult Edit([Bind(Include = "id")] UserToProject userToProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userToProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userToProject);
        }

        // GET: UserToProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserToProject userToProject = db.UsersToProjects.Find(id);
            if (userToProject == null)
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
            UserToProject userToProject = db.UsersToProjects.Find(id);
            db.UsersToProjects.Remove(userToProject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
