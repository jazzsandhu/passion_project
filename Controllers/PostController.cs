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
using passion_project.Models.viewmodel;
using System.Diagnostics;

namespace passion_project.Controllers
{
    public class PostController : Controller
    {
        private passion_context db = new passion_context();
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        // GET: Post/Details/5
        public ActionResult List()
        {
            string query = "Select * from posts";

            
            List<post> posts = db.posts.SqlQuery(query).ToList();

            return View(posts);
        }
        

        // GET: Post/Create
        public ActionResult Add()
        {
            //to create a list for users
            List<user> users = db.users.SqlQuery("select * from users").ToList();
            //to create a list for tag
            List<tag> all_tag = db.tags.SqlQuery("select * from tags").ToList();
            showpost viewmodel = new showpost();
            viewmodel.all_tag = all_tag;
            viewmodel.users = users;
            return View(viewmodel);
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Add(string post_name, string post_description ,int user_id)
        {
            //Debug.WriteLine(post_name);
            //1. get the user input 
            //run the query
            string query = "insert into posts (post_name, post_description ,user_id) values(@post_name, @post_description,@user_id) ";
            SqlParameter[] sqlparams = new SqlParameter[3];
            //bind the parameter
            sqlparams[0] = new SqlParameter("@post_name", post_name);
            sqlparams[1] = new SqlParameter("@post_description", post_description);
            sqlparams[2] = new SqlParameter("@user_id", user_id);
            //now put these values in the database
            db.Database.ExecuteSqlCommand(query, sqlparams);

            

            return RedirectToAction("List");
        }
        public ActionResult Show(int id)
        {
            //firstly show the detail of the single post
            string query_post = "select * from posts where post_id = @post_id";
            var post_parameter = new SqlParameter("@post_id" , id);
            post post = db.posts.SqlQuery(query_post, post_parameter).FirstOrDefault();

            //now the list of tag which is used by post
            string list_query = "select *from tags inner join tagposts ON tags.tag_id = tagposts.tag_tag_id where tagposts.post_post_id=@id";
            var list_parameter = new SqlParameter("@id", id);
            List<tag> related_tag = db.tags.SqlQuery(list_query, list_parameter).ToList();

            //give the list of all the tags
            string query_tag = "select * from tags ";
            List<tag> all_tag = db.tags.SqlQuery(query_tag).ToList();
            //create a model with refering the dscribed view model
            showpost viewmodel = new showpost();
            viewmodel.post = post;
            viewmodel.tags = related_tag;
            viewmodel.all_tag = all_tag;

            return View(viewmodel);


           
        }

        // GET: Post/Edit/5
        public ActionResult Update(int id)
        {
            post selectedpost = db.posts.SqlQuery("select * from posts where post_id= @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<tag> tags = db.tags.SqlQuery("select *from tags inner join tagposts ON tags.tag_id = tagposts.tag_tag_id where tagposts.post_post_id=@id", new SqlParameter("@id", id)).ToList();

            //string query_tag = "select * from tags ";
            //List<tag> all_tag = db.tags.SqlQuery(query_tag).ToList();

            List<user> users = db.users.SqlQuery("select * from users").ToList();

            //all list of the tag
            List<tag> all_tag = db.tags.SqlQuery("select * from tags").ToList();

            showpost viewmodel = new showpost();
            viewmodel.tags = tags;
            viewmodel.post = selectedpost;
            viewmodel.users = users;
            viewmodel.all_tag = all_tag;
            return View(viewmodel);
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Update(int id, string post_name, string post_description , int user_id)
        {
            string query = "update posts set post_name = @post_name , post_description = @post_description , user_id = @user_id where post_id = @id";
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@post_name", post_name);
            sqlparams[1] = new SqlParameter("@post_description", post_description);
            sqlparams[2] = new SqlParameter("@user_id", user_id);
            sqlparams[3] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
           
        }
       

        // POST: Post/Delete/5
       
        public ActionResult Delete(int id)
        {
            //write the query
            string query = "delete from posts where post_id = @post_id";
            //store the parameter in the array
            SqlParameter[] sqlparams = new SqlParameter[1];
            //bind parameter
            sqlparams[0] = new SqlParameter("@post_id", id);
            //execute the command
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //take back us to the list
            return RedirectToAction("List");

        }
        //this is for attach the tag to the post
        public ActionResult AttachTag(int id, int tag_id)
        {
            string check_query = "select * from tags inner join tagposts on tags.tag_id = tagposts.tag_tag_id where tag_tag_id = @tag_id and post_post_id= @id";
            SqlParameter[] checkparams = new SqlParameter[2];
            checkparams[0] = new SqlParameter("@tag_id", tag_id);
            checkparams[1] = new SqlParameter("@id", id);
            List<tag> tags = db.tags.SqlQuery(check_query, checkparams).ToList();

            //only execute add if that tag wasnt there
            if (tags.Count <= 0)
            {
                string query = "insert into tagposts (tag_tag_id , post_post_id) values (@tag_id, @id)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@tag_id", tag_id);
                sqlparams[1] = new SqlParameter("@id", id);

                db.Database.ExecuteSqlCommand(query, sqlparams);

            }
            return RedirectToAction("Update/" +id);


        }
        public ActionResult DetachTag(int id, int tag_id)
        {
            string query = "delete from tagposts where post_post_id = @id and tag_tag_id = @tag_id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@tag_id", tag_id);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("Update/" + id);

        }
    }
}
