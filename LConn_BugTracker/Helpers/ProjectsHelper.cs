using LConn_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LConn_BugTracker.Helpers
{
    public class ProjectsHelper : CommonHelper
    {
        
        public List<string> UsersInRoleOnProject (int projectId, string roleName)
        {
            var people = new List<string>();

            foreach(var user in UsersOnProject(projectId).ToList())
            {
                if(rolesHelper.IsUserInRole(user.Id, roleName))
                {
                    people.Add(user.Id);
                }
            }
            return people;
        }

        public bool IsUserOnProject(string userId, int projectId)
        {
            var project = db.Projects.Find(projectId);
            var flag = project.Users.Any(u => u.Id == userId);
            return (flag);
        }
        public ICollection<Project> ListUserProjects(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);

            var projects = user.Projects.ToList();
            return (projects);
        }
        public void AddUserToProject(string userId, int projectId)
        {
            if (!IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var newUser = db.Users.Find(userId);

                proj.Users.Add(newUser);
                db.SaveChanges();
            }
        }
        public void RemoveUserFromProject(string userId, int projectId)
        {
            if(IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var delUser = db.Users.Find(userId);

                proj.Users.Remove(delUser);
                db.Entry(proj).State = EntityState.Modified;  //just saves this obj instance
                db.SaveChanges();
            }
        }
        public ICollection<ApplicationUser>UsersOnProject(int projectId)
        {
            return db.Projects.Find(projectId).Users;
        }
        public ICollection<ApplicationUser>UsersNotOnProject(int projectId)
        {
            return db.Users.Where(u => u.Projects.All(p => p.ID != projectId)).ToList();
        }
    }
}