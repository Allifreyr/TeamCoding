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
        public int ide { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string userId { get; set; }

        public int projectId { get; set; }

    }
}