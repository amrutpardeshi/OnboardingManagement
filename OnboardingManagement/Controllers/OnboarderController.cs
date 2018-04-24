﻿﻿using System;
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
    public class OnboarderController : Controller
    {
        private OnboardingManagementDb db = new OnboardingManagementDb();

        public ActionResult Index()
        {
            PopulateReportingManagers();
            return View();
        }

        private void PopulateReportingManagers()
        {
            ViewData["ReportingManager"] = db.ReportingManagers;
        }


        public ActionResult Onboarders_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Onboarder> onboarders = db.Onboarders;
            DataSourceResult result = onboarders.ToDataSourceResult(request, onboarder => new {
                O_Id = onboarder.O_Id,
                O_Name = onboarder.O_Name,
                O_Rotation_Num = onboarder.O_Rotation_Num,
                RM_Id = onboarder.RM_Id
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Onboarders_Create([DataSourceRequest]DataSourceRequest request, Onboarder onboarder)
        {
            if (ModelState.IsValid)
            {
                var entity = new Onboarder
                {
                    O_Name = onboarder.O_Name,
                    O_Rotation_Num = onboarder.O_Rotation_Num,
                    RM_Id = onboarder.RM_Id
                };

                db.Onboarders.Add(entity);
                db.SaveChanges();
                onboarder.O_Id = entity.O_Id;
            }

            return Json(new[] { onboarder }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Onboarders_Update([DataSourceRequest]DataSourceRequest request, Onboarder onboarder)
        {
            if (ModelState.IsValid)
            {
                var entity = new Onboarder
                {
                    O_Id = onboarder.O_Id,
                    O_Name = onboarder.O_Name,
                    O_Rotation_Num = onboarder.O_Rotation_Num,
                    RM_Id = onboarder.RM_Id
                };

                db.Onboarders.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { onboarder }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Onboarders_Destroy([DataSourceRequest]DataSourceRequest request, Onboarder onboarder)
        {
            if (ModelState.IsValid)
            {
                var entity = new Onboarder
                {
                    O_Id = onboarder.O_Id,
                    O_Name = onboarder.O_Name,
                    O_Rotation_Num = onboarder.O_Rotation_Num,
                    RM_Id = onboarder.RM_Id
                };

                db.Onboarders.Attach(entity);
                db.Onboarders.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { onboarder }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}