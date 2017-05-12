using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamCode.Models.Entities
{
    public class File
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Name of file is required!")]
        [Display(Name = "File name")]
        public string fileName { get; set; }
        public string content { get; set; }
        [Required(ErrorMessage = "Filetype is required!")]
        [Display(Name = "File type")]
        public string fileType { get; set; }

        public virtual Project project { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}