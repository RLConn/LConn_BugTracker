using LConn_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LConn_BugTracker.Helpers
{
    public class TicketDecisionHelper : CommonHelper
    {
        public static bool TicketIsEditableByUser(Ticket ticket)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = RoleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Developer":
                    return ticket.AssignedToUserId == userId;
                case "Submitter":
                    return ticket.OwnerUserId == userId;
                case "ProjectManager":
                    return Db.Users.Find(userId).Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(ticket.Id);
                case "Admin":
                    return true;
                default:
                    return false;
            }
        }

        public static bool CommentCanBeCreatedByUser(Ticket ticket)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = RoleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Developer":
                    return ticket.AssignedToUserId == userId;
                case "Submitter":
                    return ticket.AssignedToUserId == userId;
                case "ProjectManager":
                    return Db.Users.Find(userId).Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(ticket.Id);
                case "Admin":
                    return true;
                default:
                    return false;
            }
        }

        public static bool TicketCommentIsEditableByUser(Ticket ticketComment)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = RoleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Developer":
                    return ticketComment.AssignedToUserId == userId;
                case "Submitter":
                    return ticketComment.AssignedToUserId == userId;
                case "ProjectManager":
                    return Db.Users.Find(userId).Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(ticketComment.Id);
                case "Admin":
                    return true;
                default:
                    return false;
            }
        }

        public static bool TicketTypeIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketStatusIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketPriorityIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketTitleIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketDescrptionIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketAssignedToUserIdIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

    }   
}
