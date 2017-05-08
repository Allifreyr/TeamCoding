using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamCode.Models.Entities
{
    public class Project
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Name of project is required!")]
        public string projectName { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}