using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace passion_project.Models
{
    public class post
    {
        [Key]
        public int post_id { get; set; }
        public string post_name { get; set; }
        public string post_description { get; set; }

        //one user write many posts
        public int user_id { get; set;}
        [ForeignKey("user_id")]
        public virtual user user { get; set; }

       

        //many post have many tags
        public ICollection<tag> tags { get; set; }
        

    }
}