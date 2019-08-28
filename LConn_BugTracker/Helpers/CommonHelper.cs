using LConn_BugTracker.Enumerations;
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
        protected static ApplicationDbContext Db = new ApplicationDbContext();
        protected static UserRolesHelper RoleHelper = new UserRolesHelper();
        protected static ProjectsHelper ProjectHelper = new ProjectsHelper();
        protected ApplicationUser CurrentUser = null;
        protected SystemRole CurrentRole = SystemRole.None;

        protected CommonHelper()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if(userId != null)
                CurrentUser = Db.Users.Find(userId);

            //"Submitter" ==> SystemRole.Submitter
            var stringRole = RoleHelper.ListUserRoles(userId).FirstOrDefault();

            if (!string.IsNullOrEmpty(stringRole))
                CurrentRole = (SystemRole)Enum.Parse(typeof(SystemRole), stringRole);
        }
    }
}