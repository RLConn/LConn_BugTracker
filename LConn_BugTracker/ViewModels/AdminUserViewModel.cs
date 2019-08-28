using LConn_BugTracker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LConn_BugTracker
{
    public class AdminUserViewModel
    {
        public ApplicationUserManager User { get; set; }
        public MultiSelectList Roles { get; set; }
        public string [] SelectedRoles { get; set; }
    }
}