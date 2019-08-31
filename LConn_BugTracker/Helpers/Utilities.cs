using LConn_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LConn_BugTracker.Helpers
{
    public static class Utilities
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string MakeReadable(string property, string value)
        {
            switch (property)
            {
                case "TicketStatusId":
                    return db.TicketStatuses.Find(Convert.ToInt32(value)).Name;
                case "TicketPriorityId":
                    return db.TicketPriorities.Find(Convert.ToInt32(value)).Name;
                case "TicketTypeId":
                    return db.TicketTypes.Find(Convert.ToInt32(value)).Name;
                case "AssignedToUserId":
                    if (value != null)
                        return db.Users.Find(value).FullerName;
                    return "-- UnAssigned --";
                default:
                    return value;
            }
        }
    }
}