﻿using System;
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
                {
                    instance = new UserToProjectsService();
                }

                return instance;
            }
        }

        public List<UserToProjects> GetProjectsSharedWithUser(string userId)
        {
            var projectsShared = _db.UsersToProjects.Where(up => up.user.Id == userId).ToList();

            return projectsShared;
        }

        public List<UserToProjects> GetUserWithProjectID(int projectId)
        {
            var projectsShared = _db.UsersToProjects.Where(up => up.project.id == projectId).ToList();

            return projectsShared;
        }

        public List<UserToProjects> GetProjectById(int? fileID)
        {
            var utp = _db.UsersToProjects.Where(up => up.project.id == fileID).ToList();

            return utp;
        }
    }
}