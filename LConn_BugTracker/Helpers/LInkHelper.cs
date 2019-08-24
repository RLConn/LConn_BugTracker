using LConn_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LConn_BugTracker.Helpers
{
    public class LinkHelper : CommonHelper
    {

        public bool UserCanEditTicket(Ticket ticket)
        {
            switch (this.CurrentRole)
            {
                case "Admin":
                    return true;
                case "ProjectManager":
                    //Is this Ticket on one of my Projects?
                    return CurrentUser.Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(ticket.Id);
                case "Developer":
                    return ticket.AssignedToUserId == CurrentUser.Id;
                case "Submitter":
                    return ticket.OwnerUserId == CurrentUser.Id;
                default:
                    return false;
            }

        }

    }
}