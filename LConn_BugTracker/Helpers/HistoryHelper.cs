using LConn_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace LConn_BugTracker.Helpers
{
    public class HistoryHelper : CommonHelper
    {
        public void RecordHistory(Ticket oldTicket, Ticket newTicket)
        {
            var trackedProperties = WebConfigurationManager.AppSettings["TrackedHistoryProperties"].Split(',').ToList();

            foreach (var property in newTicket.GetType().GetProperties())
            {
                //If the current property is NOT one of the properties I am interested in then move on...

                //This iteration of the foreach loop deals with the 'Updated' property...
                //I am NOT interested in making history records if this property has changed because it is not in the list
                if (!trackedProperties.Contains(property.Name))
                    continue;

                var oldPropValue = (property.GetValue(oldTicket, null) ?? "").ToString();
                var newPropValue = (property.GetValue(newTicket, null) ?? "").ToString();

                if(oldPropValue != newPropValue)
                {
                    var newHistory = new TicketHistory
                    {
                        PropertyName = property.Name,
                        OldValue = Utilities.MakeReadable(property.Name, oldPropValue),
                        NewValue = Utilities.MakeReadable(property.Name, newPropValue),
                        Updated = newTicket.Updated.GetValueOrDefault(DateTime.Now),
                        TicketId = newTicket.Id,
                        UserId = HttpContext.Current.User.Identity.GetUserId()
                    };
                    Db.TicketHistories.Add(newHistory);
                }
            }
            Db.SaveChanges();
        }
    }
}