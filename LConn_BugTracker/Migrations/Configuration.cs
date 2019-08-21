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

    internal sealed class Configuration : DbMigrationsConfiguration<LConn_BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
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

            #region User Generation
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "LeeC@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "LeeC@Mailinator.com",
                    Email = "LeeC@Mailinator.com",
                    FirstName = "Lee",
                    LastName = "Connelly",
                    DisplayName = "The LC"
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
                    DisplayName = "Smit"
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
                    DisplayName = "Devel"
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
                    DisplayName = "ProMan"
                }, "P@ssw0rd");
            }

            var adminId = userManager.FindByEmail("LeeC@mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var subId = userManager.FindByEmail("Submitter@mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");

            var deveId = userManager.FindByEmail("Developer@mailinator.com").Id;
            userManager.AddToRole(deveId, "Developer");

            var projId = userManager.FindByEmail("ProjectManager@mailinator.com").Id;
            userManager.AddToRole(projId, "ProjectManager");
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
            var blogProjectId = context.Projects.FirstOrDefault(p => p.Name == "LConn Blog").ID;
            var porfolioProjectId = context.Projects.FirstOrDefault(p => p.Name == "LConn Portfolio").ID;
            var bugTrackerProjectId = context.Projects.FirstOrDefault(p => p.Name == "LConn Bugtracker").ID;

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
                new TicketStatus { Name = "UnAssigned", Description = "Open Ticket that is not currently assigned to anything." },
                new TicketStatus { Name = "New / Assigned", Description = "New Ticket has just recently been assigned." },
                new TicketStatus { Name = "Assigned", Description = "Open Ticket that has been assigned but is not currently in progress." },
                new TicketStatus { Name = "In Progress", Description = "Ticket is in progress and is currently being worked." },
                new TicketStatus { Name = "Completed", Description = "Ticket has now been completed and closed." },
                new TicketStatus { Name = "Archived", Description = "Closed and completed ticket held for record." }
                );

            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                new TicketType { Name = "Bug", Description = "" },
                new TicketType { Name = "Defect", Description = "" },
                new TicketType { Name = "Feature Request", Description = "" },
                new TicketType { Name = "Documentation Request", Description = "" },
                new TicketType { Name = "Training Request", Description = "" },
                new TicketType { Name = "Complaint", Description = "" },
                new TicketType { Name = "Other", Description = "" }
                );
            #endregion
        }
    }
}
