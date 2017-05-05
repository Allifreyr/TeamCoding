using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamCode.Models.Entities
{
    public class File
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public int projectId { get; set;  }

        //Vantar inn data, af hvaða týpu á það að vera? (Kata)
    }
}