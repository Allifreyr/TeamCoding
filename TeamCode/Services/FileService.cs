using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamCode.Models;
using TeamCode.Models.Entities;

namespace TeamCode.Services
{
    public class FileService
    {
        private static FileService instance;

        public static FileService Instance
        {
            get
            {
                if (instance == null)
                    instance = new FileService();
                return instance;
            }
        }
        private ApplicationDbContext _db;

        public FileService()
        {
            _db = new ApplicationDbContext();
        }

        internal File AddNewFile()
        {
            throw new NotImplementedException();
        }

        public List<File> GetFilesByProject(int projectID)
        {
             var filesByProject = (from f in _db.Files where f.project.id == projectID select f).ToList();

            return filesByProject;
        }

        public List<File> GetAllFiles()
        {
            var fileList = (from f in _db.Files select f).ToList();
            return fileList;
        }

        public List<File> GetFileByID(int fileID)
        {
            var fileListByID = (from f in _db.Files where f.id == fileID select f).ToList();

            return fileListByID;
        }
        public void AddNewFile(string userId)
        {
            File file = new File();
            file.fileName = "Untitled";
            file.user = (from u in _db.Users
                            where u.Id == userId
                            select u).SingleOrDefault();
            _db.Files.Add(file);
            _db.SaveChanges();
        }
    }
}