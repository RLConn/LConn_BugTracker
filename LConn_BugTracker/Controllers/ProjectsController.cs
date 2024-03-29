﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LConn_BugTracker.Helpers;
using LConn_BugTracker.Models;

namespace LConn_BugTracker.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectsHelper projectHelper = new ProjectsHelper();
        private NotificationHelper notifHelper = new NotificationHelper();
        private HistoryHelper historyHelper = new HistoryHelper();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }
        
        // GET: Projects/Full List
        [Authorize(Roles = "ProjectManager, Admin")]
        public ActionResult FullIndex()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            //Give my details view a MultiSelectList of available PM's
            //KEEP AN EYE OUT FOR 'MAGIC STRINGS' THROUGHOUT YOUR APPLICATION...
            //MAGIC STRINGS ARE HARD CODED STRINGS THAT SHOULD BE HANDELED DIFFERENTLY...
            var allProjectManagers = roleHelper.UsersInRole("ProjectManager");
            var currentProjectManagers = projectHelper.UsersInRoleOnProject(project.Id, "ProjectManager");
            ViewBag.ProjectManagers = new MultiSelectList(allProjectManagers, "Id", "DisplayName", currentProjectManagers);

            var allSubmitters = roleHelper.UsersInRole("Submitter");
            var currentSubmitters = projectHelper.UsersInRoleOnProject(project.Id, "Submitter");
            ViewBag.Submitters = new MultiSelectList(allSubmitters, "Id", "DisplayName", currentSubmitters);

            var allDevelopers = roleHelper.UsersInRole("Developer");
            var currentDevelopers = projectHelper.UsersInRoleOnProject(project.Id, "Developer");
            ViewBag.Developers = new MultiSelectList(allDevelopers, "Id", "DisplayName", currentDevelopers);

            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "ProjectManager, Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Created")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "ProjectManager, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Users = new MultiSelectList(db.Users.ToList(), "Id", "FullName", projectHelper.UsersOnProject(project.Id).Select(u => u.Id));
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Created")] Project project, List<string> users)
        {
            if (ModelState.IsValid)
            {
                var oldProject = db.Projects.AsNoTracking().FirstOrDefault(p => p.Id == project.Id);

                var newProject = db.Projects.Find(project.Id);
                newProject.Name = project.Name;
                newProject.Description = project.Description;
                newProject.Updated = DateTime.Now;

               
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
