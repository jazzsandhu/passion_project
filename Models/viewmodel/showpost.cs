using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace passion_project.Models.viewmodel
{
    public class showpost
    {
        //one post
        public virtual post post { get; set; }
        public List<post> posts { get; set; }
        //a list of every tag related to that post
        public List<tag> tags { get; set; }
        //a list of every tag they can select 
        public List<tag> all_tag { get; set; }
        //list of the user
        public List<user> users { get; set; }

    }
}