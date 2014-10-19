using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeGuru.Models;

namespace TimeGuru.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectModels db = new ProjectModels();
        //
        // GET: /Mongo/

        public ActionResult Index()
        {
            var collection = from f in db.Projects
                             select f;
            return View(collection);
        }

        //
        // GET: /Mongo/Details/5

        public ActionResult Details(string id)
        {
            Project dep = (from f in db.Projects
                           where f._id == new ObjectId(id)
                           select f).First();
            return View(dep);
        }

        //
        // GET: /Mongo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Mongo/Create

        [HttpPost]
        public ActionResult Create(Project project)
        {
            try
            {
                db.CreateProject(project);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Mongo/Edit/5

        public ActionResult Edit(string name)
        {
            List<Project> list = (from f in db.Projects
                                  where f.Name == name
                                  select f).ToList();
            Project project = new Project();
            if (list.Count > 0) project = list[0];
            return View(project);
        }

        //        // POST: /Mongo/Edit/5

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            try
            {
                db.EditProject(project);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // Get: /Mongo/Delete/5
        public ActionResult Delete(string name)
        {
            try
            {
                Project dep = (from f in db.Projects
                               where f.Name == name
                               select f).First();
                db.DeleteProject(dep);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
