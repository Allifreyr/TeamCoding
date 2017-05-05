using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamCode.Models.Entities
{
    public class UserToProject
    {
        [Key]
        public int id { get; set; }

        public virtual ApplicationUser user { get; set; }
        public virtual Project project { get; set; }
    }
}