using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
                if(instance == null)
                {
                    instance = new ProjectService();
                }
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
            _db = new ApplicationDbContext();
            var userProject = _db.Projects.Where(t => t.user.Id == userID).ToList();

            return userProject;
        }

        public List<Project> GetAllProject()
        {
            var projectList = (from p in _db.Projects select p).ToList();
            return projectList;
        }

        public Project GetProjectByID(int projectID)
        {
            var projectByID = (from p in _db.Projects where p.id == projectID select p).SingleOrDefault();

            return projectByID;
        }

        public int AddNewProject(string userId)
        {
            Project project = new Project();
            project.projectName = "Untitled";
            project.user = (from u in _db.Users
                            where u.Id == userId
                            select u).SingleOrDefault();

            _db.Projects.Add(project);
            _db.SaveChanges();

            return project.id;
        }



    }
}