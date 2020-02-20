using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//for make our context file work
using System.Data.Entity;

namespace passion_project.Data
{
    public class passion_context:DbContext
    {

        public passion_context() : base("name=passion_context")
        {

        }
        //this line allow us to create db in mssql according to the models.
        public System.Data.Entity.DbSet<passion_project.Models.user> users { get; set; }
        public System.Data.Entity.DbSet<passion_project.Models.tag> tags { get; set; }
        public System.Data.Entity.DbSet<passion_project.Models.post> posts { get; set; }
    }
}