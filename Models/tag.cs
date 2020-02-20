using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace passion_project.Models
{
    public class tag
    {
        [Key]
        public int tag_id { get; set; }
        public string tag_name { get; set; }
        public string tag_description { get; set; }
        public ICollection<post> posts { get; set; }
    }
}