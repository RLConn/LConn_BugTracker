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
            var seededUserPassword = WebConfigurationManager.AppSettings["SeededUserPassword"];
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
                }, seededUserPassword);
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
                    AvatarUrl = "/Avatars/Dany.jpg"
                }, seededUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "NewProjectManager@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "NewProjectManager@Mailinator.com",
                    Email = "NewProjectManager@Mailinator.com",
                    FirstName = "New",
                    LastName = "Manager",
                    DisplayName = "NewPm",
                    AvatarUrl = "/Avatars/Jon Snow.jpg"
                }, seededUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "OldProjectManager@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "OldProjectManager@Mailinator.com",
                    Email = "OldProjectManager@Mailinator.com",
                    FirstName = "Old",
                    LastName = "Manager",
                    DisplayName = "OldPm",
                    AvatarUrl = "/Avatars/nightking avatar.jpg"
                }, seededUserPassword);
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
                    AvatarUrl = "/Avatars/cersei.jpg"
                }, seededUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "NewSubmitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "NewSubmitter@Mailinator.com",
                    Email = "NewSubmitter@Mailinator.com",
                    FirstName = "New",
                    LastName = "Submitter",
                    DisplayName = "NewSub",
                    AvatarUrl = "/Avatars/sansa.jpg"
                }, seededUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "OldSubmitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "OldSubmitter@Mailinator.com",
                    Email = "OldSubmitter@Mailinator.com",
                    FirstName = "Old",
                    LastName = "Submitter",
                    DisplayName = "OldSub",
                    AvatarUrl = "/Avatars/bran.jpg"
                }, seededUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "ThisSubmitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ThisSubmitter@Mailinator.com",
                    Email = "ThisSubmitter@Mailinator.com",
                    FirstName = "This",
                    LastName = "Submitter",
                    DisplayName = "ThisSub",
                    AvatarUrl = "/Avatars/drogo.jpg"
                }, seededUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "ThatSubmitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ThatSubmitter@Mailinator.com",
                    Email = "ThatSubmitter@Mailinator.com",
                    FirstName = "That",
                    LastName = "Submitter",
                    DisplayName = "ThatSub",
                    AvatarUrl = "/Avatars/tormund.jpg"
                }, seededUserPassword);
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
                    AvatarUrl = "/Avatars/tyrion.jpg"
                }, seededUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "NewDeveloper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "NewDeveloper@Mailinator.com",
                    Email = "NewDeveloper@Mailinator.com",
                    FirstName = "New",
                    LastName = "Developer",
                    DisplayName = "NewDev",
                    AvatarUrl = "/Avatars/sam.jpg"
                }, seededUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "OldDeveloper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "OldDeveloper@Mailinator.com",
                    Email = "OldDeveloper@Mailinator.com",
                    FirstName = "Old",
                    LastName = "Developer",
                    DisplayName = "OldDev",
                    AvatarUrl = "/Avatars/jora.jpg"
                }, seededUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "ThisDeveloper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ThisDeveloper@Mailinator.com",
                    Email = "ThisDeveloper@Mailinator.com",
                    FirstName = "This",
                    LastName = "Developer",
                    DisplayName = "ThisDev",
                    AvatarUrl = "/Avatars/jamie.jpg"
                }, seededUserPassword);
            }

            if (!context.Users.Any(u => u.Email == "ThatDeveloper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ThatDeveloper@Mailinator.com",
                    Email = "ThatDeveloper@Mailinator.com",
                    FirstName = "That",
                    LastName = "Developer",
                    DisplayName = "ThatDev",
                    AvatarUrl = "/Avatars/arya.jpg"
                }, seededUserPassword);
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

            var nprojId = userManager.FindByEmail("NewProjectManager@mailinator.com").Id;
            userManager.AddToRole(nprojId, "ProjectManager");

            var oprojId = userManager.FindByEmail("OldProjectManager@mailinator.com").Id;
            userManager.AddToRole(oprojId, "ProjectManager");

            var subId = userManager.FindByEmail("Submitter@mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");

            var nsubId = userManager.FindByEmail("NewSubmitter@mailinator.com").Id;
            userManager.AddToRole(nsubId, "Submitter");

            var osubId = userManager.FindByEmail("OldSubmitter@mailinator.com").Id;
            userManager.AddToRole(osubId, "Submitter");

            var isubId = userManager.FindByEmail("ThisSubmitter@mailinator.com").Id;
            userManager.AddToRole(isubId, "Submitter");

            var asubId = userManager.FindByEmail("ThatSubmitter@mailinator.com").Id;
            userManager.AddToRole(asubId, "Submitter");

            var deveId = userManager.FindByEmail("Developer@mailinator.com").Id;
            userManager.AddToRole(deveId, "Developer");

            var ndeveId = userManager.FindByEmail("NewDeveloper@mailinator.com").Id;
            userManager.AddToRole(ndeveId, "Developer");

            var odeveId = userManager.FindByEmail("OldDeveloper@mailinator.com").Id;
            userManager.AddToRole(odeveId, "Developer");

            var ideveId = userManager.FindByEmail("ThisDeveloper@mailinator.com").Id;
            userManager.AddToRole(ideveId, "Developer");

            var adeveId = userManager.FindByEmail("ThatDeveloper@mailinator.com").Id;
            userManager.AddToRole(adeveId, "Developer");

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
                    new Project { Name = "Portfolio", Description = "This is the Portfolio Project that is now out in the wilds of the web.", Created = DateTime.Now },
                    new Project { Name = "BugTracker", Description = "This is the BugTracker Project that is now out in the wilds of the web.", Created = DateTime.Now},
                    new Project { Name = "Financial Advisor", Description = "This is the Financial Advisor Project that is now out in the wilds of the web.", Created = DateTime.Now },
                    new Project { Name = "Marketing Evaluation", Description = "This is the Marketing Evaluation Project that is now out in the wilds of the web.", Created = DateTime.Now },
                    new Project { Name = "Widget Sales", Description = "This is the Widget Sales Project that is now out in the wilds of the web.", Created = DateTime.Now }
                );

            context.SaveChanges();
            #endregion

            #region Project Assignment
            var bloProjectId = context.Projects.FirstOrDefault(p => p.Name == "LConn Blog").Id;
            var porProjectId = context.Projects.FirstOrDefault(p => p.Name == "Portfolio").Id;
            var bugProjectId = context.Projects.FirstOrDefault(p => p.Name == "BugTracker").Id;
            var finProjectId = context.Projects.FirstOrDefault(p => p.Name == "Financial Advisor").Id;
            var marProjectId = context.Projects.FirstOrDefault(p => p.Name == "Marketing Evaluation").Id;
            var widProjectId = context.Projects.FirstOrDefault(p => p.Name == "Widget Sales").Id;

            var projectHelper = new ProjectsHelper();

            //Assign a PM, a Sub and a Dev to the Blog project
            projectHelper.AddUserToProject(projId, bloProjectId);
            projectHelper.AddUserToProject(subId, bloProjectId);
            projectHelper.AddUserToProject(deveId, bloProjectId);

            //Assign a PM, a Sub and a Dev to the Portfolio project
            projectHelper.AddUserToProject(nprojId, porProjectId);
            projectHelper.AddUserToProject(nsubId, porProjectId);
            projectHelper.AddUserToProject(ndeveId, porProjectId);

            //Assign a PM, a Sub and a Dev  to the BugTracker project
            projectHelper.AddUserToProject(oprojId, bugProjectId);
            projectHelper.AddUserToProject(osubId, bugProjectId);
            projectHelper.AddUserToProject(odeveId, bugProjectId);

            //Assign a PM, a Sub and a Dev  to the Financial Advisor project
            projectHelper.AddUserToProject(projId, finProjectId);
            projectHelper.AddUserToProject(isubId, finProjectId);
            projectHelper.AddUserToProject(ideveId, finProjectId);

            //Assign a PM, a Sub and a Dev  to the Marketing Evaluation project
            projectHelper.AddUserToProject(nprojId, marProjectId);
            projectHelper.AddUserToProject(asubId, marProjectId);
            projectHelper.AddUserToProject(adeveId, marProjectId);

            //Assign the Demo PM, a Sub and a Dev  to the Widget Sales project
            projectHelper.AddUserToProject(demoProjId, widProjectId);
            projectHelper.AddUserToProject(demoSubId, widProjectId);
            projectHelper.AddUserToProject(demoDevId, widProjectId);

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
                new TicketStatus { Name = "New/UnAssigned", Description = "New Ticket that not yet been assigned." },
                new TicketStatus { Name = "UnAssigned", Description = "Open Ticket that is not currently assigned to anyone." },
                new TicketStatus { Name = "New/Assigned", Description = "New Ticket has just recently been assigned." },
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
                //Blog Project tickets
                new Ticket
                {
                    ProjectId = bloProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = deveId,
                    Title = "Blog Ticket #1",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Immediate").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New/Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = bloProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = deveId,
                    Title = "Blog Ticket #2",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "High").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "In Progress").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },
                new Ticket
                {
                    ProjectId = bloProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = deveId,
                    Title = "Blog Ticket #3",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Feature Request").Id,
                },
                new Ticket
                {
                    ProjectId = bloProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = deveId,
                    Title = "Blog Ticket #4",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Documentation Request").Id,
                },
                new Ticket
                {
                    ProjectId = bloProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = deveId,
                    Title = "Blog Ticket #5",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Low").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "In Progress").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Training Request").Id,
                },
                new Ticket
                {
                    ProjectId = bloProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = deveId,
                    Title = "Blog Ticket #6",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "None").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Archived").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Complaint").Id,
                },
                new Ticket
                {
                    ProjectId = bloProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = deveId,
                    Title = "Blog Ticket #7",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "None").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Archived").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Other").Id,
                },

                //Portfolio Project Tickets
                new Ticket
                {
                    ProjectId = porProjectId,
                    OwnerUserId = nsubId,
                    AssignedToUserId = ndeveId,
                    Title = "Portfolio Ticket #1",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "High").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New/Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = porProjectId,
                    OwnerUserId = nsubId,
                    AssignedToUserId = ndeveId,
                    Title = "Portfolio Ticket #2",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "High").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New/Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },
                new Ticket
                {
                    ProjectId = porProjectId,
                    OwnerUserId = nsubId,
                    AssignedToUserId = ndeveId,
                    Title = "Portfolio Ticket #3",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Low").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Feature Request").Id,
                },
                new Ticket
                {
                    ProjectId = porProjectId,
                    OwnerUserId = nsubId,
                    AssignedToUserId = ndeveId,
                    Title = "Portfolio Ticket #4",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "In Progress").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Documentation Request").Id,
                },
                new Ticket
                {
                    ProjectId = porProjectId,
                    OwnerUserId = nsubId,
                    AssignedToUserId = ndeveId,
                    Title = "Portfolio Ticket #5",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "In Progress").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Training Request").Id,
                },
                new Ticket
                {
                    ProjectId = porProjectId,
                    OwnerUserId = nsubId,
                    AssignedToUserId = ndeveId,
                    Title = "Portfolio Ticket #6",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "None").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Archived").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Complaint").Id,
                },
                new Ticket
                {
                    ProjectId = porProjectId,
                    OwnerUserId = nsubId,
                    AssignedToUserId = ndeveId,
                    Title = "Portfolio Ticket #7",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "None").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Archived").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Other").Id,
                },

                //BugTracker Tickets
                new Ticket
                {
                    ProjectId = bugProjectId,
                    OwnerUserId = osubId,
                    AssignedToUserId = odeveId,
                    Title = "BugTracker Ticket #1",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "High").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New/Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = bugProjectId,
                    OwnerUserId = osubId,
                    AssignedToUserId = odeveId,
                    Title = "BugTracker Ticket #2",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "High").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New/Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },
                new Ticket
                {
                    ProjectId = bugProjectId,
                    OwnerUserId = osubId,
                    AssignedToUserId = odeveId,
                    Title = "BugTracker Ticket #3",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "In Progress").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Feature Request").Id,
                },
                new Ticket
                {
                    ProjectId = bugProjectId,
                    OwnerUserId = osubId,
                    AssignedToUserId = odeveId,
                    Title = "BugTracker Ticket #4",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Low").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Documentation Request").Id,
                },
                new Ticket
                {
                    ProjectId = bugProjectId,
                    OwnerUserId = osubId,
                    AssignedToUserId = odeveId,
                    Title = "BugTracker Ticket #5",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Completed").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Training Request").Id,
                },
                new Ticket
                {
                    ProjectId = bugProjectId,
                    OwnerUserId = osubId,
                    AssignedToUserId = odeveId,
                    Title = "BugTracker Ticket #6",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "None").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Archived").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Complaint").Id,
                },
                new Ticket
                {
                    ProjectId = bugProjectId,
                    OwnerUserId = osubId,
                    AssignedToUserId = odeveId,
                    Title = "BugTracker Ticket #7",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "None").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Archived").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },

                //Financial Advisor Tickets
                new Ticket
                {
                    ProjectId = finProjectId,
                    OwnerUserId = isubId,
                    AssignedToUserId = ideveId,
                    Title = "Financial Advisor Ticket #1",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Immediate").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New/Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = finProjectId,
                    OwnerUserId = isubId,
                    AssignedToUserId = ideveId,
                    Title = "Financial Advisor Ticket #2",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New/Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },
                new Ticket
                {
                    ProjectId = finProjectId,
                    OwnerUserId = isubId,
                    AssignedToUserId = ideveId,
                    Title = "Financial Advisor Ticket #3",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Feature Request").Id,
                },
                new Ticket
                {
                    ProjectId = finProjectId,
                    OwnerUserId = isubId,
                    AssignedToUserId = ideveId,
                    Title = "Financial Advisor Ticket #4",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Low").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Documentation Request").Id,
                },
                new Ticket
                {
                    ProjectId = finProjectId,
                    OwnerUserId = isubId,
                    AssignedToUserId = ideveId,
                    Title = "Financial Advisor Ticket #5",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "In Progress").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Training Request").Id,
                },
                new Ticket
                {
                    ProjectId = finProjectId,
                    OwnerUserId = isubId,
                    AssignedToUserId = ideveId,
                    Title = "Financial Advisor Ticket #6",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Completed").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Complaint").Id,
                },
                new Ticket
                {
                    ProjectId = finProjectId,
                    OwnerUserId = isubId,
                    AssignedToUserId = ideveId,
                    Title = "Financial Advisor Ticket #7",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Immediate").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Completed").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },

                //Marketing Evaluation Tickets
                new Ticket
                {
                    ProjectId = marProjectId,
                    OwnerUserId = asubId,
                    AssignedToUserId = adeveId,
                    Title = "Marketing Evaluation Ticket #1",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "High").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New/Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = marProjectId,
                    OwnerUserId = asubId,
                    AssignedToUserId = adeveId,
                    Title = "Marketing Evaluation Ticket #2",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "High").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New/Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },
                new Ticket
                {
                    ProjectId = marProjectId,
                    OwnerUserId = asubId,
                    AssignedToUserId = adeveId,
                    Title = "Marketing Evaluation Ticket #3",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "In Progress").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Feature Request").Id,
                },
                new Ticket
                {
                    ProjectId = marProjectId,
                    OwnerUserId = asubId,
                    AssignedToUserId = adeveId,
                    Title = "Marketing Evaluation Ticket #4",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Low").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Documentation Request").Id,
                },
                new Ticket
                {
                    ProjectId = marProjectId,
                    OwnerUserId = asubId,
                    AssignedToUserId = adeveId,
                    Title = "Marketing Evaluation Ticket #5",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "In Progress").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Training Request").Id,
                },
                new Ticket
                {
                    ProjectId = marProjectId,
                    OwnerUserId = asubId,
                    AssignedToUserId = adeveId,
                    Title = "Marketing Evaluation Ticket #6",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Low").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Completed").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Other").Id,
                },
                new Ticket
                {
                    ProjectId = marProjectId,
                    OwnerUserId = asubId,
                    AssignedToUserId = adeveId,
                    Title = "Marketing Evaluation Ticket #7",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "None").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Archived").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Complaint").Id,
                },

                //Widget Sales Tickets
                new Ticket
                {
                    ProjectId = widProjectId,
                    OwnerUserId = demoSubId,
                    Title = "Widget Sales Ticket #1",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Immediate").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New/UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = widProjectId,
                    OwnerUserId = demoSubId,
                    AssignedToUserId = demoDevId,
                    Title = "Widget Sales Ticket #2",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "High").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New/Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },
                new Ticket
                {
                    ProjectId = widProjectId,
                    OwnerUserId = demoSubId,
                    Title = "Widget Sales Ticket #3",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Feature Request").Id,
                },
                new Ticket
                {
                    ProjectId = widProjectId,
                    OwnerUserId = demoSubId,
                    AssignedToUserId = demoDevId,
                    Title = "Widget Sales Ticket #4",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Low").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Documentation Request").Id,
                },
                new Ticket
                {
                    ProjectId = widProjectId,
                    OwnerUserId = demoSubId,
                    AssignedToUserId = demoDevId,
                    Title = "Widget Sales Ticket #5",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "In Progress").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Training Request").Id,
                },
                new Ticket
                {
                    ProjectId = widProjectId,
                    OwnerUserId = demoSubId,
                    AssignedToUserId = demoDevId,
                    Title = "Widget Sales Ticket #6",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Low").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Completed").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Complaint").Id,
                },
                new Ticket
                {
                    ProjectId = widProjectId,
                    OwnerUserId = demoSubId,
                    AssignedToUserId = demoDevId,
                    Title = "Widget Sales Ticket #7",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "None").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "Archived").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Other").Id,
                });
            context.SaveChanges();
            #endregion
        }
    }
}
