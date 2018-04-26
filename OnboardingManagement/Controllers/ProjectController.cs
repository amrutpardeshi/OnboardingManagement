﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using OnboardingManagement.Models;

namespace OnboardingManagement.Controllers
{
    public class ProjectController : Controller
    {
        private OnboardingManagementDb db = new OnboardingManagementDb();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
     

        public ActionResult Update()
        {
            return View();
        }
        /// <summary>
        /// To get a selected projects in textbox list
        ///</summary>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult getProjectById(int id)
        {
            var project = (from d in db.Projects
                          where d.P_Id == id
                          select d).FirstOrDefault(); 
            return Json(project, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// To get all projects in dropdown list
        /// </summary>
        /// <returns>all projects in json</returns>
        public JsonResult GetAllProjects()
        {
            return Json(db.Projects.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Projects_Read([DataSourceRequest]DataSourceRequest request)
        {
          var projects = db.Projects.ToList();
            DataSourceResult result = projects.ToDataSourceResult(request, project => new {
                P_Id = project.P_Id,
                P_Name = project.P_Name,
                P_Technology = project.P_Technology
            });

            return Json(result);
        }


        /// <summary>
        /// To add the new Project
        /// </summary>
       
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, Project project)
        {
            if (ModelState.IsValid)
            {
                var entity = new Project
                {
                    P_Name = project.P_Name,
                    P_Technology = project.P_Technology
                };

                db.Projects.Add(entity);
                db.SaveChanges();
                project.P_Id = entity.P_Id;

                return View();
            }

            return View("/Project/Index");
          
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Projects_Update([DataSourceRequest]DataSourceRequest request, Project project)
        {
            if (ModelState.IsValid)
            {
                var entity = new Project
                {
                    P_Id = project.P_Id,
                    P_Name = project.P_Name,
                    P_Technology = project.P_Technology
                };

                db.Projects.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { project }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Projects_Destroy([DataSourceRequest]DataSourceRequest request, Project project)
        {
            if (ModelState.IsValid)
            {
                var entity = new Project
                {
                    P_Id = project.P_Id,
                    P_Name = project.P_Name,
                    P_Technology = project.P_Technology
                };

                db.Projects.Attach(entity);
                db.Projects.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { project }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
