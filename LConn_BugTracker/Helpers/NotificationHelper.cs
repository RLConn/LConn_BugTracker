﻿using LConn_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace LConn_BugTracker.Helpers
{
    public class NotificationHelper : CommonHelper
    {
        public void ManageNotifications(Ticket oldTicket, Ticket newTicket)
        {
            CreateAssignmentNotification(oldTicket, newTicket);
            CreateChangeNotification(oldTicket, newTicket);
        }

        #region Assignment notification management
        private void CreateAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            //4 Cases
            //1. No Change
            //2. An assignment - This means the old AssignedToUserId was null and the current one is not
            //3. An unassignment - This means the old AssignedToUserIs had a value but the current one is null
            //4. A reassignment - The Ticket was assigned to someone and now it is assigned to someone else

            //setting up a few boolean variables to determine which case I am in
            var noChange = (oldTicket.AssignedToUserId == newTicket.AssignedToUserId);
            var assignment = (string.IsNullOrEmpty(oldTicket.AssignedToUserId));
            var unassignment = (string.IsNullOrEmpty(newTicket.AssignedToUserId));

            if (noChange)
                return;

            if (assignment)
                GenerateAssignmentNotification(oldTicket, newTicket);
            else if (unassignment)
                GenerateUnAssignmentNotification(oldTicket, newTicket);
            else
            {
                GenerateAssignmentNotification(oldTicket, newTicket);
                GenerateUnAssignmentNotification(oldTicket, newTicket);
            }
        }

        private void GenerateUnAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                Subject = $"You were unassigned from Ticket Id {newTicket.Id} on {DateTime.Now}",
                IsRead = false,
                RecipientId = oldTicket.AssignedToUserId,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                NotificationBody = $"Please acknowledge that you have read this notification by marking it as read",
                TicketId = newTicket.Id
            };

            Db.TicketNotifications.Add(notification);
            Db.SaveChanges();
        }


        private void GenerateAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var senderId = HttpContext.Current.User.Identity.GetUserId();
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                Subject = $"You were assigned to Ticket Id {newTicket.Id} on {DateTime.Now}",
                IsRead = false,
                RecipientId = newTicket.AssignedToUserId,
                SenderId = senderId,
                NotificationBody = $"Please acknowledge that you have read this notification by marking it Read",
                TicketId = newTicket.Id
            };

            Db.TicketNotifications.Add(notification);
            Db.SaveChanges();
        }
        #endregion

        #region Change notification management
        private void CreateChangeNotification(Ticket oldTicket, Ticket newTicket)
        {
            var messageBody = new StringBuilder();

            foreach (var property in WebConfigurationManager.AppSettings["TrackedTicketProperties"].Split(','))
            {
                var oldValue = Utilities.MakeReadable(property, oldTicket.GetType().GetProperty(property).GetValue(oldTicket, null).ToString());
                var newValue = Utilities.MakeReadable(property, newTicket.GetType().GetProperty(property).GetValue(newTicket, null).ToString());

                if (oldValue != newValue)
                {
                    messageBody.AppendLine(new string('-', 45));
                    messageBody.AppendLine($"A change was made to Property: {property}.");
                    messageBody.AppendLine($"The old value was: {oldValue.ToString()}");
                    messageBody.AppendLine($"The new value is: {newValue.ToString()}");
                }
            }

            if (!string.IsNullOrEmpty(messageBody.ToString()))
            {
                var message = new StringBuilder();
                message.AppendLine($"Changes were made to Ticket Id: {newTicket.Id} on {newTicket.Updated.GetValueOrDefault().ToString("MMM d, yyy")}");
                message.AppendLine(messageBody.ToString());
                var senderId = HttpContext.Current.User.Identity.GetUserId();

                var notification = new TicketNotification
                {
                    TicketId = newTicket.Id,
                    Created = DateTime.Now,
                    Subject = $"Ticket Id: {newTicket.Id} has changed",
                    RecipientId = oldTicket.AssignedToUserId,
                    SenderId = senderId,
                    NotificationBody = message.ToString(),
                    IsRead = false
                };

                Db.TicketNotifications.Add(notification);
                Db.SaveChanges();
            }
        }
        #endregion

        #region Dashboard notifiation helpers
        public static int GetNewUserNotificationCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return Db.TicketNotifications.Where(t => t.RecipientId == userId && !t.IsRead).Count();
        }

        public static int GetAllUserNoticationCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return Db.TicketNotifications.Where(t => t.RecipientId == userId).Count();
        }

        public static List<TicketNotification> GetUnreadUserNotifications()
        { 
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return Db.TicketNotifications.Where(t => t.RecipientId == userId && !t.IsRead).ToList();
        }
        #endregion
    }
}