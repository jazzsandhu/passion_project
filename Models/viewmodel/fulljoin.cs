using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace passion_project.Models.viewmodel
{
    public class fulljoin
    {
        public List<user> users { get; set; }
        public List<tag> tags { get; set; }
        public List<post> posts { get; set; }
    }
}