using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using passion_project.Data;
using passion_project.Models;
using passion_project.Models.viewmodel;
using System.Diagnostics;

namespace passion_project.Controllers
{
    public class JoinController : Controller
    {
        private passion_context db = new passion_context();
        // GET: Join
        public ActionResult Index()
        {
            return View();
        }

        // GET: Join/Details/5
        public ActionResult Search(string key)
        {
            string query_t = "";
            string query_p = "";
            string query_u = "";

           

            if (key != "")
            {
                query_t = "select * from tags";
                query_p = "select * from posts";
                query_u = "select * from users";
                query_t = query_t + " where tag_name like '%" + key + "%'";
                query_p = query_p + " where post_name like '%" + key + "%'";
                query_u = query_u + " where user_name like '%" + key + "%'"; 

            }
            else
            {
                return RedirectToAction("Error");
            }
           
            List<post> posts = db.posts.SqlQuery(query_p).ToList();
            List<tag> tags = db.tags.SqlQuery(query_t).ToList();
            List<user> users = db.users.SqlQuery(query_u).ToList();

            fulljoin viewmodel = new fulljoin();
            viewmodel.users = users;
            viewmodel.tags = tags;
            viewmodel.posts = posts;

            return View(viewmodel);
        }

        // GET: Join/Create
        public ActionResult Error( string key)
        {
            return View();
        }

        // POST: Join/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Join/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Join/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Join/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Join/Delete/5
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
