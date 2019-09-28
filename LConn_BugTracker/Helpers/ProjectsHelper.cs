using LConn_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LConn_BugTracker.Helpers
{
    public class ProjectsHelper
    {
        protected UserRolesHelper RoleHelper = new UserRolesHelper();
        protected ApplicationDbContext Db = new ApplicationDbContext();

        public List<string> UsersInRoleOnProject (int projectId, string roleName)
        {
            var people = new List<string>();

            foreach(var user in UsersOnProject(projectId).ToList())
            {
                if(RoleHelper.IsUserInRole(user.Id, roleName))
                {
                    people.Add(user.Id);
                }
            }
            return people;
        }

        public bool IsUserOnProject(string userId, int projectId)
        {
            var project = Db.Projects.Find(projectId);
            var flag = project.Users.Any(u => u.Id == userId);
            return (flag);
        }
        public ICollection<Project> ListUserProjects(string userId)
        {
            ApplicationUser user = Db.Users.Find(userId);

            var projects = user.Projects.ToList();
            return (projects);
        }

        public int CountUserProjects(string userId)
        {
            ApplicationUser user = Db.Users.Find(userId);

        
            return (user.Projects.Count());
        }

        public void AddUserToProject(string userId, int projectId)
        {
            if (!IsUserOnProject(userId, projectId))
            {
                Project proj = Db.Projects.Find(projectId);
                var newUser = Db.Users.Find(userId);

                proj.Users.Add(newUser);
                Db.SaveChanges();
            }
        }
        public void RemoveUserFromProject(string userId, int projectId)
        {
            if(IsUserOnProject(userId, projectId))
            {
                Project proj = Db.Projects.Find(projectId);
                var delUser = Db.Users.Find(userId);

                proj.Users.Remove(delUser);
                Db.Entry(proj).State = EntityState.Modified;  //just saves this obj instance
                Db.SaveChanges();
            }
        }
        public ICollection<ApplicationUser>UsersOnProject(int projectId)
        {
            return Db.Projects.Find(projectId).Users;
        }
        public ICollection<ApplicationUser>UsersNotOnProject(int projectId)
        {
            return Db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList();
        }
    }
}