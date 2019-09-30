using LConn_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LConn_BugTracker.Helpers
{
    public class TicketHelper : CommonHelper
    {
        public List<Ticket>GetTicketsByPriority(string name)
        {
            return Db.Tickets.Where(t => t.TicketPriority.Name == name).ToList();
        }

        public List<Ticket> GetTicketsByStatus(string name)
        {
            return Db.Tickets.Where(t => t.TicketStatus.Name == name).ToList();
        }

        public List<Ticket> GetTicketsByType(string name)
        {
            return Db.Tickets.Where(t => t.TicketType.Name == name).ToList();
        }


        public int GetTicketTotalCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return Db.Tickets.Where(t => t.AssignedToUserId == userId).Count();
        }

        public int GetTicketCount(string status)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = RoleHelper.ListUserRoles(userId).FirstOrDefault();
            if (status == "All")
            {
                switch (myRole)
                {
                    case "Developer":
                        return Db.Tickets.Where(t => t.AssignedToUserId == userId).Count();
                    case "Submitter":
                        return Db.Tickets.Where(t => t.OwnerUserId == userId).Count();
                    case "ProjectManager":
                        return Db.Users.Find(userId).Projects.SelectMany(t => t.Tickets).Count();
                    case "Admin":
                        return Db.Tickets.Count();
                    default:
                        return 0;
                }
            }
            else
            {
                switch (myRole)
                {
                    case "Developer":
                        return Db.Tickets.Where(t => t.TicketStatus.Name == status && t.AssignedToUserId == userId).Count();
                    case "Submitter":
                        return Db.Tickets.Where(t => t.TicketStatus.Name == status && t.OwnerUserId == userId).Count();
                    case "ProjectManager":
                        var pmTickets = Db.Users.Find(userId).Projects.SelectMany(t => t.Tickets);
                        return pmTickets.Where(t => t.TicketStatus.Name == status).Count();
                    case "Admin":
                        return Db.Tickets.Where(t => t.TicketStatus.Name == status).Count();
                    default:
                        return 0;
                }
            }
        }

    }
}