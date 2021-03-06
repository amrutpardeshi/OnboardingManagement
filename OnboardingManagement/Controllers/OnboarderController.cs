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
    [Authorize(Roles = "Admin")]
    public class OnboarderController : Controller
    {
        OnboardingManagementDb db = new OnboardingManagementDb();
        [Authorize(Roles = "Admin")]
        public ActionResult Add(Onboarder onboarder) {
            if (onboarder.O_Name==null)
            {
                ViewBag.msg = "*Name is compulsory!!!";
                return View("Index");
            }
            onboarder.O_Rotation_Num = 1;
            db.Onboarders.Add(onboarder);
            db.SaveChanges();
            return View("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Update(String onboardId, String Onboard_Name, String ReportingManagerID) {
            if (Onboard_Name .Equals(""))
            {
                ViewBag.msg = "*Name is compulsory!!!";
                return View("Index");
            }
            int oid = Convert.ToInt32(onboardId);
            Onboarder onboarder = db.Onboarders.FirstOrDefault(m => m.O_Id == oid);
            onboarder.RM_Id = Convert.ToInt32(ReportingManagerID);
            onboarder.O_Name = Onboard_Name;
            db.SaveChanges();

            return View("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(String OnboardID)
        {

            int oid = Convert.ToInt32(OnboardID);
            Onboarder onboarder = db.Onboarders.FirstOrDefault(m => m.O_Id == oid);
            db.Onboarders.Remove(onboarder);
            db.SaveChanges();
            return View("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {

            PopulateReportingManagers();
            return View();
        }

        /// <summary>
        /// To use text at foreign key
        /// </summary>
        private void PopulateReportingManagers()
        {
            ViewData["ReportingManager"] = db.ReportingManagers;
        }

        /// <summary>
        /// To get all onboarders in dropdown list
        /// </summary>
        /// <returns>all onboarders in json</returns>
        public JsonResult GetAllOnboarders()
        {
            //OnboardingManagementDb db1 = new OnboardingManagementDb();
            
            return Json(db.Onboarders, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllOnboarders1([DataSourceRequest]DataSourceRequest request)
        {
            //OnboardingManagementDb db1 = new OnboardingManagementDb();
            var V = db.Onboarders;
            return Json(V.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult getOnboarder(int id)
        {
            var result = db.Onboarders.FirstOrDefault(m => m.O_Id == id);
            return Json(result);
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
