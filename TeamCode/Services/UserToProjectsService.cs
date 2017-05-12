using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamCode.Models;
using TeamCode.Models.Entities;
using TeamCode.Models.ViewModels;

namespace TeamCode.Services
{
    public class UserToProjectsService
    {

        private static UserToProjectsService instance;
        private ApplicationDbContext _db = new ApplicationDbContext();

        public static UserToProjectsService Instance
        {
            get
            {
                if(instance == null)
                    instance = new UserToProjectsService();
                return instance;
            }
        }

        public List<UserToProjects> GetProjectsSharedWithUser(string userId)
        {

            //  _db = new ApplicationDbContext();
            var projectsShared = _db.UsersToProjects.Where(up => up.user.Id == userId).ToList();

            /*         var projectsShared = (from up in _db.UsersToProjects
                                           where up.user.Id == userId
                                           select up).ToList();*/

            return projectsShared;
        }

        public List<UserToProjects> GetUserWithProjectID(int? projectId)
        {
            var projectsShared = _db.UsersToProjects.Where(up => up.project.id == projectId).ToList();

            return projectsShared;
        }

        public UserToProjects FindUserWithProjectID(int? projectId)
        {
            var projectsShared = _db.UsersToProjects.Find(projectId);

            return projectsShared;
        }

        public List<UserToProjects> GetProjectById(int? fileID)
        {
            var utp = _db.UsersToProjects.Where(up => up.project.id == fileID).ToList();
            return utp;
        }

        public UserToProjects NewUserToProject(int? id)
        {
            UserToProjects up = new UserToProjects
            {
                project = (from p in _db.Projects
                           where p.id == id.Value
                           select p).SingleOrDefault(),
                user = null
            };
            return up;
        }

        public UserToProjectsViewModel NewUserToProjectViewModel(UserToProjects up)
        {
            UserToProjectsViewModel upvm = new UserToProjectsViewModel
            {
                ide = up.id,
                projectId = up.project.id,
                userId = null
            };
            return upvm;
        }

        public List<UserToProjects> userInTable(UserToProjectsViewModel userToProject)
        {
            var uit = (from p in _db.UsersToProjects
                       where p.user.Email == userToProject.userId
                       select p).ToList();
            return uit;
        }

        public UserToProjects UserToProjectList(UserToProjectsViewModel userToProject)
        {
            var emailId = UserToProjectsService.Instance.CompareUserEmail(userToProject);
            UserToProjects upvm = new UserToProjects
            {
                id = userToProject.ide,
                project = (from p in _db.Projects
                           where p.id == userToProject.projectId
                           select p).SingleOrDefault(),
                user = emailId
            };
            _db.UsersToProjects.Add(upvm);
            _db.SaveChanges();

            return upvm;
        }

        public List<ApplicationUser> userExists(UserToProjectsViewModel userToProject)
        {
            var ue = (from p in _db.Users
                      where p.Email == userToProject.userId
                      select p).ToList();
            return ue;
        }

        public ApplicationUser CompareUserEmail(UserToProjectsViewModel userToProject)
        {
            var emailId = _db.Users.Where(bla => bla.Email == userToProject.userId).SingleOrDefault();

            return emailId;
        }

        public void SaveUserToProject(UserToProjects userProj)
        {
            Project dbProj = _db.Projects.Where(f => f.id == userProj.id).SingleOrDefault();
            dbProj.id = userProj.id;

            _db.Entry(dbProj).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void DeleteUserToProject(int? id)
        {
            UserToProjects up = UserToProjectsService.Instance.FindUserWithProjectID(id);
            _db.UsersToProjects.Remove(up);
            _db.Entry(up).State = EntityState.Deleted;
            _db.SaveChanges();
        }
    }


}