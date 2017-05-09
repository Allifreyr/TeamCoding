using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamCode.Models.ViewModels
{
    public class UserToProjectsViewModel
    {
        [Key]
        public int id { get; set; }

        public string userId { get; set; }
        public int projectId { get; set; }

        
    }
}