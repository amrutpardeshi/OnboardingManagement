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
    public class ReportingManagerController : Controller
    {
        private OnboardingManagementDb db = new OnboardingManagementDb();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

      
        public JsonResult GetAllReportingManager()
        {
            return Json(db.ReportingManagers.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReportingManagers_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<ReportingManager> reportingmanagers = db.ReportingManagers;
            DataSourceResult result = reportingmanagers.ToDataSourceResult(request, reportingManager => new {
                RM_Id = reportingManager.RM_Id,
                RM_Name = reportingManager.RM_Name
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReportingManagers_Create([DataSourceRequest]DataSourceRequest request, ReportingManager reportingManager)
        {
            if (ModelState.IsValid)
            {
                var entity = new ReportingManager
                {
                    RM_Name = reportingManager.RM_Name
                };

                db.ReportingManagers.Add(entity);
                db.SaveChanges();
                reportingManager.RM_Id = entity.RM_Id;
            }

            return Json(new[] { reportingManager }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReportingManagers_Update([DataSourceRequest]DataSourceRequest request, ReportingManager reportingManager)
        {
            if (ModelState.IsValid)
            {
                var entity = new ReportingManager
                {
                    RM_Id = reportingManager.RM_Id,
                    RM_Name = reportingManager.RM_Name
                };

                db.ReportingManagers.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { reportingManager }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReportingManagers_Destroy([DataSourceRequest]DataSourceRequest request, ReportingManager reportingManager)
        {
            if (ModelState.IsValid)
            {
                var entity = new ReportingManager
                {
                    RM_Id = reportingManager.RM_Id,
                    RM_Name = reportingManager.RM_Name
                };

                db.ReportingManagers.Attach(entity);
                db.ReportingManagers.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { reportingManager }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
