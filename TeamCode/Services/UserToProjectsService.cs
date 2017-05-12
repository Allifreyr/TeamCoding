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

        public List<UserToProjects> GetUserWithProjectID(int projectId)
        {
            var projectsShared = _db.UsersToProjects.Where(up => up.project.id == projectId).ToList();

            return projectsShared;
        }
    }


}