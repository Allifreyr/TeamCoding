using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamCode.Models;
using TeamCode.Models.Entities;

namespace TeamCode.Services
{
    public class ProjectService
    {
        private static ProjectService instance;

        public static ProjectService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProjectService();
                return instance;
            }
        }

        private ApplicationDbContext _db;

        public ProjectService()
        {
            _db = new ApplicationDbContext();
        }

        internal Project AddNewProject()
        {
            throw new NotImplementedException();
        }

        public List<Project> GetProjectsByUser(string userID)
        {
            var userProject = (from p in _db.Projects where p.user.Id == userID select p).ToList();
           
            return userProject;
        }

        public List<Project> GetAllProject()
        {
            var projectList = (from p in _db.Projects select p).ToList();
            return projectList;
        }

        public List<Project> GetProjectByID(int projectID)
        {
            var projectListByID = (from p in _db.Projects where p.id == projectID select p).ToList();

            return projectListByID;
        }
        
        public void AddNewProject(string userId)
        {
            Project project = new Project();
            project.projectName = "Untitled";
            project.user = (from u in _db.Users
                            where u.Id == userId
                            select u).SingleOrDefault();

            _db.Projects.Add(project);
            _db.SaveChanges();
        }
        
    }
}