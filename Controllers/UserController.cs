using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using passion_project.Data;
using passion_project.Models;
//using passion_project.Models.ViewModels;
using System.Diagnostics;

namespace passion_project.Controllers
{
    
    public class UserController : Controller
    {
        private passion_context db = new passion_context();
        // GET: User 
        public ActionResult Index()
        {
            return View();
            
        }

        // GET: Users list
        public ActionResult List()
        {
           // Debug.WriteLine("this is user list ");
            List<user> users = db.users.SqlQuery("select * from users").ToList();
            return View(users);
        }

        // GET: User/Create
        public ActionResult Add()
        {
            return View();
        }
        //url /user/add in layout 
        // POST: User/Create
        [HttpPost]
        public ActionResult Add(string first_name, string last_name, string e_mail, string phone_number, string home_address, string user_name)
        {
            Debug.WriteLine(first_name);
            //1. get the user input 
            //run the query
            string query = "insert into users (first_name,last_name,e_mail,phone_number,home_address,user_name) values(@first_name, @last_name,@e_mail,@phone_number,@home_address,@user_name) ";
            SqlParameter[] sqlparams = new SqlParameter[6];
            //bind the parameter
            sqlparams[0] = new SqlParameter("@first_name", first_name);
            sqlparams[1] = new SqlParameter("@last_name", last_name);
            sqlparams[2] = new SqlParameter("@e_mail", e_mail);
            sqlparams[3] = new SqlParameter("@phone_number", phone_number);
            sqlparams[4] = new SqlParameter("@home_address", home_address);
            sqlparams[5] = new SqlParameter("@user_name", user_name);
            //now put these values in the database
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
            
        }
        //show the user
        public ActionResult Show(int? id)
        {
            
            if (id == null)
            {
                Debug.WriteLine("this is error"+ id);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user users = db.users.SqlQuery("select * from users where user_id = @user_id", new SqlParameter("@user_id", id)).FirstOrDefault();
            if(users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: User/Edit/5
        public ActionResult Update(int id)
        {
            string query = "select * from users where user_id= @user_id";
            SqlParameter sqlparams = new SqlParameter("@user_id", id);
            user selecteduser = db.users.SqlQuery(query, sqlparams).FirstOrDefault();

            return View(selecteduser);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Update(int id, string first_name, string last_name, string e_mail, string phone_number, string home_address, string user_name)
        {
            string query = "update users SET first_name = @first_name, last_name = @last_name, e_mail = @e_mail, phone_number= @phone_number, home_address = @home_address, user_name = @user_name where user_id=@user_id";
            SqlParameter[] sqlparams = new SqlParameter[7];
            //bind the parameter
            sqlparams[0] = new SqlParameter("@first_name", first_name);
            sqlparams[1] = new SqlParameter("@last_name", last_name);
            sqlparams[2] = new SqlParameter("@e_mail", e_mail);
            sqlparams[3] = new SqlParameter("@phone_number", phone_number);
            sqlparams[4] = new SqlParameter("@home_address", home_address);
            sqlparams[5] = new SqlParameter("@user_name", user_name);
            sqlparams[6] = new SqlParameter("@user_id", id);
           
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        // GET: User delete with id
        public ActionResult Delete(int id)
        {
            //write the query
            string query = "delete from users where user_id = @user_id";
            //store the parameter in the array
            SqlParameter[] sqlparams = new SqlParameter[1];
            //bind parameter
            sqlparams[0] = new SqlParameter("@user_id", id);
            //execute the command
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //take back us to the list
            return RedirectToAction("List");
            
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
