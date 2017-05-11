using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
                if(instance == null)
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

        public List<File> GetFilesByProject(int? projectID)
        {
            _db = new ApplicationDbContext();
            var filesByProject = _db.Files.Where(f => f.project.id == projectID).ToList();

            return filesByProject;
        }

        internal dynamic GetFileProjectID(int? fileID)
        {
            var fileByID = (from f in _db.Files where f.id == fileID select f).SingleOrDefault();

            return fileByID.project;
        }

        internal string GetFileName(int? fileID)
        {
            var fileByID = (from f in _db.Files where f.id == fileID select f).SingleOrDefault();

            return fileByID.fileName;
        }

        internal dynamic GetFileUserID(int? fileID)
        {
            File fileByID = _db.Files.Find(fileID);

            //(from f in _db.Files where f.id == fileID select f).SingleOrDefault();

            return fileByID.user;
        }

        internal string GetFileType(int? fileID)
        {
            var fileByID = (from f in _db.Files where f.id == fileID select f).SingleOrDefault();

            return fileByID.fileType;
        }

        public string GetValueFromContent(int? fileID)
        {
            var fileByID = (from f in _db.Files where f.id == fileID select f).SingleOrDefault();

            return fileByID.content;
        }

        public List<File> GetAllFiles()
        {
            var fileList = (from f in _db.Files select f).ToList();
            return fileList;
        }

        public File GetFileByID(int fileID)
        {
            //var fileByID = (from f in _db.Files where f.id == fileID select f).SingleOrDefault();
            var fileByID = _db.Files.Where(f => f.id == fileID).SingleOrDefault();

            return fileByID;
        }

        public File PostFileByID(File file)
        {
            //var fileByID = (from f in _db.Files where f.id == fileID select f).SingleOrDefault();
            File dbFile = _db.Files.Where(f => f.id == file.id).SingleOrDefault();
            if(dbFile.id == file.id)
            {
                dbFile.content = file.content;
                dbFile.fileName = file.fileName;
                dbFile.fileType = file.fileType;
                _db.Entry(dbFile).State = EntityState.Modified;
                _db.SaveChanges();
                return dbFile;
            }

            return null;
        }

        public void SetValueToContent(int? fileID, string content)
        {

            var fileByID = (from f in _db.Files where f.project.id == fileID select f).SingleOrDefault();
            fileByID.content = content;

            _db.SaveChanges();
        }

        public void AddNewFile(string userId, int projectId)
        {
            File file = new File();
            file.project = (from p in _db.Projects
                            where p.id == projectId
                            select p).SingleOrDefault();
            file.fileName = "Untitled";
            file.fileType = ".js";
            file.content = "Vei þetta virkaði! - Hello world og eitthvað þannig..";
            file.user = (from u in _db.Users
                         where u.Id == userId
                         select u).SingleOrDefault();
            _db.Files.Add(file);
            _db.SaveChanges();
        }

        public void AddNewFile(string userId, int projectId, string fileName, string fileType)
        {
            File file = new File();
            file.project = (from p in _db.Projects
                            where p.id == projectId
                            select p).SingleOrDefault();
            file.fileName = fileName;
            file.fileType = fileType;
            file.content = "Vei virkadi - Hello World og eitthvad solleidis :D !!!";
            file.user = (from u in _db.Users
                         where u.Id == userId
                         select u).SingleOrDefault();
            _db.Files.Add(file);
            _db.SaveChanges();
        }
    }
}