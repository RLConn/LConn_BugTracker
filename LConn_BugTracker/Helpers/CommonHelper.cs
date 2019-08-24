using LConn_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LConn_BugTracker.Helpers
{
    public abstract class CommonHelper
    {
        protected ApplicationDbContext Db = new ApplicationDbContext();
        protected UserRolesHelper RoleHelper = new UserRolesHelper();
        protected ProjectsHelper ProjectHelper = new ProjectsHelper();
        protected ApplicationUser CurrentUser = null;
        protected String CurrentRole = "";

        protected CommonHelper()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            CurrentUser = Db.Users.Find(userId);
            CurrentRole = RoleHelper.ListUserRoles(CurrentUser.Id).FirstOrDefault();
        }
    }
}