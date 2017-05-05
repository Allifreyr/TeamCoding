﻿using System;
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
        public string fileName { get; set; }
        public string content { get; set; }
        public string fileType { get; set; }

        public virtual Project project { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}