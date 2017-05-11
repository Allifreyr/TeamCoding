using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamCode.Models
{
    public class FileViewModel
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Name of file is required!")]
        public string fileName { get; set; }
        [AllowHtml]
        public string content { get; set; }
        [Required(ErrorMessage = "Filetype is required!")]
        public string fileType { get; set; }

        public int projectid { get; set; }
        public string userid { get; set; }
    }
}