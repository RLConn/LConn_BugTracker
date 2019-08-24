using LConn_BugTracker.Helpers;
using LConn_BugTracker.Models;
using LConn_BugTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LConn_BugTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectsHelper projectHelper = new ProjectsHelper();

        // GET: Admin
        public ActionResult UserIndex()
        {
            var users = db.Users.Select(u => new UserProfileViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DisplayName = u.DisplayName,
                AvatarUrl = u.AvatarUrl,
                Email = u.Email
            }).ToList();

            return View(users);
        }

        public ActionResult ManageUserRole(string userId)
        {
            //How do I load up a DropDownList with Role information??
            //new SelectList("The list of data pushed int the control",
            //               "The column that will be used to communicate my selection(s) to the post,
            //               "The column that we show the user inside the control",
            //               "If they already occupy a role, show this instead of nothing")
            var currentRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            ViewBag.UserId = userId;
            ViewBag.UserRole = new SelectList(db.Roles.ToList(), "Name", "Name", currentRole);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRole(string userId, string userRole)
        {
            //This is where I will be using the UserRolesHelper class to make sure my user occupies the proper role
            //The first thing I want to do is make sure I remove the user from ALL Roles they may occupy
            foreach(var role in roleHelper.ListUserRoles(userId))
            {
                roleHelper.RemoveUserFromRole(userId, role);
            }

            //If the incoming role selection is not null, I want to assign the user to the selected role
            if(! string.IsNullOrEmpty(userRole))
            {
                roleHelper.AddUserToRole(userId, userRole);
            }

            return RedirectToAction("UserIndex");
        }

        public ActionResult ManageRoles()
        {
            var users = db.Users.Select(u => new UserProfileViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DisplayName = u.DisplayName,
                AvatarUrl = u.AvatarUrl,
                Email = u.Email
            }).ToList();

            ViewBag.Users = new MultiSelectList(db.Users.ToList(), "Id", "FullNameWithEmail");
            ViewBag.RoleName = new SelectList(db.Roles.ToList(), "Name", "Name");

            return View(users);
        }

        [HttpPost]
        public ActionResult ManageRoles(List<string> users, string roleName)
        {
            if (users != null)
            {
                //Lets iterate over the incoming list of Users that were selected from the form
                foreach (var userId in users)
                {
                    //and remove each of them from whatever role they occupy only to add them back to the selected role
                    foreach (var role in roleHelper.ListUserRoles(userId))
                    {
                        roleHelper.RemoveUserFromRole(userId, role);
                    }

                    //Only to add the back to the selected role
                    if (!string.IsNullOrEmpty(roleName))
                    {
                        roleHelper.AddUserToRole(userId, roleName);
                    }
                }
            }

            return RedirectToAction("ManageRoles");
        }

        public ActionResult ManageUserProjects(string userId)
        {
            var myProjects = projectHelper.ListUserProjects(userId).Select(p => p.Id);
            ViewBag.Projects = new MultiSelectList(db.Projects.ToList(), "Id", "Name", myProjects);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserProjects(List<int> projects, string userId)
        {
            foreach (var project in projectHelper.ListUserProjects(userId).ToList())
            {
                projectHelper.RemoveUserFromProject(userId, project.Id);
            }

            if (projects != null)
            {
                foreach (var projectId in projects)
                {
                    projectHelper.AddUserToProject(userId, projectId);
                }
            }

            return RedirectToAction("UserIndex");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectUsers(int projectId, List<string> ProjectManagers, List<string> Developers, List<string> Submitters)
        {
            //Step 1: Remove all users from the project
            foreach (var user in projectHelper.UsersOnProject(projectId).ToList())
            {
                projectHelper.RemoveUserFromProject(user.Id, projectId);
            }

            //Step 2: Adds back all the selected PM's
            if (ProjectManagers != null)
            {
                foreach (var projectManagerId in ProjectManagers)
                {
                    projectHelper.AddUserToProject(projectManagerId, projectId);
                }
            }

            //Step 3: Adds back all the selected Developers
            if (Developers != null)
            {
                foreach (var developerId in Developers)
                {
                    projectHelper.AddUserToProject(developerId, projectId);
                }
            }

            //Step 4: Adds back all the selected Submitters
            if (Submitters != null)
            {
                foreach (var submitterId in Submitters)
                {
                    projectHelper.AddUserToProject(submitterId, projectId);
                }
            }

            //Step 4: Redirect the user somewhere
            return RedirectToAction("Details", "Projects", new { id = projectId });
        }
    }
}
