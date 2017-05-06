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
        private ApplicationDbContext _db;

        public FileService()
        {
            _db = new ApplicationDbContext();
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
            var fileListByID = (from p in _db.Files where p.id == fileID select p).ToList();

            return fileListByID;
        }
    }
}