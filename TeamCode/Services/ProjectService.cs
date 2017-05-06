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
        private ApplicationDbContext _db;

        public ProjectService()
        {
            _db = new ApplicationDbContext();
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
        /*
        public void AddNewProject()
        {
            Project project = new Project();
            project.projectName = "test";
            //project.user = ;
            
            //_db.Projects.Insertstuff
            //_db.submitstuff
        }
        */
    }
}