using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace passion_project.Models
{
    public class user
    {
        [Key]
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string e_mail { get; set; }
        public string phone_number { get; set; }
        public string home_address { get; set; }
        public string user_name { get; set; }

        //its for mant post in one user
        public ICollection <post> posts { get; set; }
    }
}