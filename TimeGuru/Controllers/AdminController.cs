using TimeGuru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeGuru.Controllers
{
    public class AdminController : Controller
    {
        DBEntities db = new DBEntities();
        ProjectModels projectModel = new ProjectModels();
        //
        // GET: /Mongo/

        public class AdminModel 
        {
        public bool Project { get;set;}
        public bool Task { get;set;}
        public bool UserProjects { get;set;}
        public List<Project> projects { get; set; }
        }

        public ActionResult Index()
        {
           // var model = new AdminModel();
           // model.Project = true;
           // var collection = from f in projectModel.Projects select f;
            //model.projects=new List<Project>(collection);
           // return View(model);
           var collection = from f in db.Departments select f;
           return View(collection);
        }

        //
        // GET: /Mongo/Details/5

        public ActionResult Details(int id)
        {
            Department dep = (from f in db.Departments
                              where f.DepartmentId == id
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
        public ActionResult Create(Department project)
        {
            try
            {
                db.CreateDepartment(project);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Mongo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            List<Department> list = (from f in db.Departments
                                     where f.DepartmentId == id
                                     select f).ToList();
            Department project = new Department();
            if (list.Count > 0) project = list[0];
            return View(project);
        }

        //
        // POST: /Mongo/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Department project)
        {
            try
            {
                db.EditDepartment(project);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Mongo/Delete/5

        public ActionResult Delete(int id)
        {
            Department dep = (from f in db.Departments
                              where f.DepartmentId == id
                              select f).First();
            return View(dep);
        }

        //
        // POST: /Mongo/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Department collection)
        {
            try
            {
                Department dep = (from f in db.Departments
                                  where f.DepartmentId == id
                                  select f).First();
                db.DeleteDepartment(dep);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (db == null) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
