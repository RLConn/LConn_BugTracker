namespace LConn_BugTracker.Migrations
{
    using LConn_BugTracker.Helpers;
    using LConn_BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<LConn_BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "LConn_BugTracker.Models.ApplicationDbContext";
        }

        protected override void Seed(LConn_BugTracker.Models.ApplicationDbContext context)
        {
            #region roleManager
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            #endregion

            #region User Creation
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "LeeC@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "LeeC@Mailinator.com",
                    Email = "LeeC@Mailinator.com",
                    FirstName = "Lee",
                    LastName = "Connelly",
                    DisplayName = "The LC",
                    AvatarUrl = "/Avatars/Another pic (2).jpg"
                }, "P@ssw0rd");
            }

            if (!context.Users.Any(u => u.Email == "Submitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Submitter@Mailinator.com",
                    Email = "Submitter@Mailinator.com",
                    FirstName = "Sub",
                    LastName = "Mitter",
                    DisplayName = "Smit",
                    AvatarUrl = "/Avatars/defaultavatar.jpg"
                }, "P@ssw0rd");
            }

            if (!context.Users.Any(u => u.Email == "Developer@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Developer@Mailinator.com",
                    Email = "Developer@Mailinator.com",
                    FirstName = "Deve",
                    LastName = "Loper",
                    DisplayName = "Devel",
                    AvatarUrl = "/Avatars/defaultavatar.jpg"
                }, "P@ssw0rd");
            }

            if (!context.Users.Any(u => u.Email == "ProjectManager@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ProjectManager@Mailinator.com",
                    Email = "ProjectManager@Mailinator.com",
                    FirstName = "Proj",
                    LastName = "Manag",
                    DisplayName = "ProMan",
                    AvatarUrl = "/Avatars/defaultavatar.jpg"
                }, "P@ssw0rd");
            }

            //Adding in Demo Users
            var demoUserPassword = WebConfigurationManager.AppSettings["DemoUserPassword"];
            if (!context.Users.Any(u => u.Email == "DemoAdmin@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@Mailinator.com",
                    Email = "DemoAdmin@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "The Admin",
                    AvatarUrl = "/Avatars/defaultavatar.jpg"
                }, demoUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "DemoProjectManager@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoProjectManager@Mailinator.com",
                    Email = "DemoProjectManager@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Project Manager",
                    DisplayName = "DemoPM",
                    AvatarUrl = "/Avatars/defaultavatar.jpg"
                }, demoUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "DemoSubmitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@Mailinator.com",
                    Email = "DemoSubmitter@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "DemoSub",
                    AvatarUrl = "/Avatars/defaultavatar.jpg"
                }, demoUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "DemoDeveloper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@Mailinator.com",
                    Email = "DemoDeveloper@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "DemoDev",
                    AvatarUrl = "/Avatars/defaultavatar.jpg"
                }, demoUserPassword);
            }
                    
            #endregion

            #region Role Assignment
            var adminId = userManager.FindByEmail("LeeC@mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var projId = userManager.FindByEmail("ProjectManager@mailinator.com").Id;
            userManager.AddToRole(projId, "ProjectManager");

            var subId = userManager.FindByEmail("Submitter@mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");

            var deveId = userManager.FindByEmail("Developer@mailinator.com").Id;
            userManager.AddToRole(deveId, "Developer");


            //Demo Role Assignment
            var demoAdminId = userManager.FindByEmail("DemoAdmin@Mailinator.com").Id;
            userManager.AddToRole(demoAdminId, "Admin");

            var demoProjId = userManager.FindByEmail("DemoProjectManager@Mailinator.com").Id;
            userManager.AddToRole(demoProjId, "ProjectManager");

            var demoSubId = userManager.FindByEmail("DemoSubmitter@Mailinator.com").Id;
            userManager.AddToRole(demoSubId, "Submitter");

            var demoDevId = userManager.FindByEmail("DemoDeveloper@Mailinator.com").Id;
            userManager.AddToRole(demoDevId, "Developer");

            #endregion

            #region Project Creation
            context.Projects.AddOrUpdate(
                p => p.Name,
                    new Project { Name = "LConn Blog", Description = "This the Blog Project that is now out in the wilds of the web.", Created = DateTime.Now },
                    new Project { Name = "LConn Portfolio", Description = "This is the Portfolio Project that is now out in the wilds of the web.", Created = DateTime.Now },
                    new Project { Name = "LConn BugTracker", Description = "This is the BugTracker Project that is now out in the wilds of the web.", Created = DateTime.Now}
                );

            context.SaveChanges();
            #endregion

            #region Project Assignment
            var blogProjectId = context.Projects.FirstOrDefault(p => p.Name == "LConn Blog").Id;
            var porfolioProjectId = context.Projects.FirstOrDefault(p => p.Name == "LConn Portfolio").Id;
            var bugTrackerProjectId = context.Projects.FirstOrDefault(p => p.Name == "LConn Bugtracker").Id;

            var projectHelper = new ProjectsHelper();

            //Assign all three users to the Blog project
            projectHelper.AddUserToProject(subId, blogProjectId);
            projectHelper.AddUserToProject(deveId, blogProjectId);
            projectHelper.AddUserToProject(projId, blogProjectId);

            //Assign all three users to the Portfolio project
            projectHelper.AddUserToProject(subId, porfolioProjectId);
            projectHelper.AddUserToProject(deveId, porfolioProjectId);
            projectHelper.AddUserToProject(projId, porfolioProjectId);

            //Assign all three users to the BugTracker project
            projectHelper.AddUserToProject(subId, bugTrackerProjectId);
            projectHelper.AddUserToProject(deveId, bugTrackerProjectId);
            projectHelper.AddUserToProject(projId, bugTrackerProjectId);
            #endregion

            #region Priority, Status & Type Creation (required FK's for a Ticket)
            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                new TicketPriority { Name = "Immediate", Description = "Highest priority level requiring immediate action" },
                new TicketPriority { Name = "High", Description = "A high priority level requiring quick action" },
                new TicketPriority { Name = "Medium", Description = "A secondary priority level requires action once higher priority issues have been handled" },
                new TicketPriority { Name = "Low", Description = "Not crucial, but to be looked at when time allows" },
                new TicketPriority { Name = "None", Description = "To be assigned" }
                );

            context.TicketStatuses.AddOrUpdate(
                t => t.Name,
                new TicketStatus { Name = "New / UnAssigned", Description = "New Ticket that not yet been assigned." },
                new TicketStatus { Name = "UnAssigned", Description = "Open Ticket that is not currently assigned to anyone." },
                new TicketStatus { Name = "New / Assigned", Description = "New Ticket has just recently been assigned." },
                new TicketStatus { Name = "Assigned", Description = "Open Ticket that has been assigned but is not currently in progress." },
                new TicketStatus { Name = "In Progress", Description = "Ticket is in progress and is currently being worked." },
                new TicketStatus { Name = "Completed", Description = "Ticket has now been completed and closed." },
                new TicketStatus { Name = "Archived", Description = "Closed and completed ticket held for record." }
                );

            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                new TicketType { Name = "Bug", Description = "An error has occurred that resulted in either the application crashing or the user seeing error information" },
                new TicketType { Name = "Defect", Description = "An error has occurred that resulted in either a miscalculation or an incorrect workflow" },
                new TicketType { Name = "Feature Request", Description = "A client has called in asking for some new functionality in an existing application" },
                new TicketType { Name = "Documentation Request", Description = "A client has called in asking for new documentation to be created for the existing application" },
                new TicketType { Name = "Training Request", Description = "A client has called in asking to schedule a training session" },
                new TicketType { Name = "Complaint", Description = "A client has called in to make a general complaint about our application" },
                new TicketType { Name = "Other", Description = "A call has been received that requires follow up but is outside the usual parameters for a request" }
                );

            context.SaveChanges();
            #endregion

            #region Ticket Creation
            context.Tickets.AddOrUpdate(
                p => p.Title,
                //1 unassigned Bug on the Blog Project
                //1 assigned Defect on the Blog Project
                new Ticket
                {
                    ProjectId = blogProjectId,
                    OwnerUserId = subId,
                    Title = "Seeded Ticket #1",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = blogProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = deveId,
                    Title = "Seeded Ticket #2",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },

                //1 unassigned Bug on the Portfolio Project
                //1 assigned Defect on the Portfolio Project
                new Ticket
                {
                    ProjectId = porfolioProjectId,
                    OwnerUserId = subId,
                    Title = "Seeded Ticket #3",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = porfolioProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = deveId,
                    Title = "Seeded Ticket #4",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },

                //1 unassigned Bug on the BugTracker
                //1 assigned Defect on the BugTracker
                new Ticket
                {
                    ProjectId = bugTrackerProjectId,
                    OwnerUserId = subId,
                    Title = "Seeded Ticket #5",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = bugTrackerProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = deveId,
                    Title = "Seeded Ticket #6",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                });
            context.SaveChanges();
            #endregion
        }
    }
}
