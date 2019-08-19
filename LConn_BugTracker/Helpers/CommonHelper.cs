using LConn_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LConn_BugTracker.Helpers
{
    public abstract class CommonHelper
    {
        protected static ApplicationDbContext db = new ApplicationDbContext();
        protected static UserRolesHelper rolesHelper = new UserRolesHelper();
        protected static ProjectsHelper projectsHelper = new ProjectsHelper();
    }
}