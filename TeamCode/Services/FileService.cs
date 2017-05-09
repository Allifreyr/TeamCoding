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
            var filesByProject = (from f in _db.Files where f.project.id == projectID select f).ToList();

            return filesByProject;
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
            var fileByID = (from f in _db.Files where f.id == fileID select f).SingleOrDefault();

            return fileByID;
        }

        public void SetValueToContent(int? fileID, string content)
        {

            var fileByID = (from f in _db.Files where f.project.id == fileID select f).SingleOrDefault();
            fileByID.content = content;
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
            file.user = (from u in _db.Users
                         where u.Id == userId
                         select u).SingleOrDefault();
            _db.Files.Add(file);
            _db.SaveChanges();
        }
    }
}