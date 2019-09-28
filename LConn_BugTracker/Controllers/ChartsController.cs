using LConn_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LConn_BugTracker.ChartViewModel;

namespace LConn_BugTracker.Controllers
{
    public class ChartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Charts
        public JsonResult GetMorrisData1()
        {
            var dataSet = new List<MorrisBarChartData>();
            
            foreach(var ticketPriority in db.TicketPriorities.ToList())
            {
                var count = db.TicketPriorities.Find(ticketPriority.Id).Tickets.Count();
                dataSet.Add(new MorrisBarChartData { label = ticketPriority.Name, value = count });
            }

            return Json(dataSet);
        }

        public JsonResult GetMorrisData2()
        {
            var dataSet = new List<MorrisBarChartData>();

            foreach (var ticketType in db.TicketTypes.ToList())
            {
                var count = db.TicketTypes.Find(ticketType.Id).Tickets.Count();
                dataSet.Add(new MorrisBarChartData { label = ticketType.Name, value = count });
            }

            return Json(dataSet);
        }

        public JsonResult GetMorrisData3()
        {
            var dataSet = new List<MorrisBarChartData>();

            foreach (var ticketStatus in db.TicketStatuses.ToList())
            {
                var count = db.TicketStatuses.Find(ticketStatus.Id).Tickets.Count();
                dataSet.Add(new MorrisBarChartData { label = ticketStatus.Name, value = count });
            }

            return Json(dataSet);
        }

    }
}