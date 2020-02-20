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
    public class TagController : Controller
    {
        private passion_context db = new passion_context();
        // GET: Tag
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tag list
        public ActionResult List(string key)
        {
            string query = "select * from tags";
            // Debug.WriteLine("this is tag list ");
            if (key != "")
            {
                query = query + " where tag_name like '%" + key + "%'";
            }
            List<tag> tags = db.tags.SqlQuery(query).ToList();
            return View(tags);
        }
        //show the tag
        public ActionResult Show(int? id)
        {

            if (id == null)
            {
                Debug.WriteLine("this is error" + id);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tag tags = db.tags.SqlQuery("select * from tags where tag_id = @tag_id", new SqlParameter("@tag_id", id)).FirstOrDefault();
            if (tags == null)
            {
                return HttpNotFound();
            }
            return View(tags);
        }
        // GET: Tag/Create
        public ActionResult Add()
        {
            return View();
        }

        // POST: Tag/Create
        [HttpPost]
        public ActionResult Add(string tag_name, string tag_description)
        {
            Debug.WriteLine(tag_name);
            //1. get the user input 
            //run the query
            string query = "insert into tags (tag_name, tag_description ) values(@tag_name, @tag_description) ";
            SqlParameter[] sqlparams = new SqlParameter[2];
            //bind the parameter
            sqlparams[0] = new SqlParameter("@tag_name", tag_name);
            sqlparams[1] = new SqlParameter("@tag_description", tag_description);
            
            //now put these values in the database
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");

        }

        // GET: Tag/Edit/5
        public ActionResult Update(int id)
        {
            string query = "select * from tags where tag_id= @tag_id";
            SqlParameter sqlparams = new SqlParameter("@tag_id", id);
            tag selectedtag = db.tags.SqlQuery(query, sqlparams).FirstOrDefault();

            return View(selectedtag);
        }

        // POST: Tag/Edit/5
        [HttpPost]
        public ActionResult Update(int id, string tag_name, string tag_description)
        {
            string query = "update tags SET tag_name = @tag_name, tag_description = @tag_description where tag_id=@tag_id";
            SqlParameter[] sqlparams = new SqlParameter[3];
            //bind the parameter
            sqlparams[0] = new SqlParameter("@tag_name", tag_name);
            sqlparams[1] = new SqlParameter("@tag_description", tag_description);
            sqlparams[2] = new SqlParameter("@tag_id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            //write the query
            string query = "delete from tags where tag_id = @tag_id";
            //store the parameter in the array
            SqlParameter[] sqlparams = new SqlParameter[1];
            //bind parameter
            sqlparams[0] = new SqlParameter("@tag_id", id);
            //execute the command
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //take back us to the list
            return RedirectToAction("List");

        }

        // POST: Tag/Delete/5
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
