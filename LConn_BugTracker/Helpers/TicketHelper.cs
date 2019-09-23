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


        public static int GetTicketTotalCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return Db.Tickets.Where(t => t.AssignedToUserId == userId).Count();
        }

       
    }
}