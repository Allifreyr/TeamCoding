using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamCode.Models;

namespace TeamCode.Services
{
    public class UserToProjectsService
    {

        private static UserToProjectsService instance;

        public static UserToProjectsService Instance
        {
            get
            {
                if(instance == null)
                    instance = new UserToProjectsService();
                return instance;
            }
        }

        private ApplicationDbContext _db;
    }


}