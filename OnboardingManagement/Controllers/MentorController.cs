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
    public class MentorController : Controller
    {
        private OnboardingManagementDb db = new OnboardingManagementDb();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllMentors()
        {
            return Json(db.Mentors.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Mentors_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Mentor> mentors = db.Mentors;
            DataSourceResult result = mentors.ToDataSourceResult(request, mentor => new {
                M_Id = mentor.M_Id,
                M_Name = mentor.M_Name
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Mentors_Create([DataSourceRequest]DataSourceRequest request, Mentor mentor)
        {
            if (ModelState.IsValid)
            {
                var entity = new Mentor
                {
                    M_Name = mentor.M_Name
                };

                db.Mentors.Add(entity);
                db.SaveChanges();
                mentor.M_Id = entity.M_Id;
            }

            return Json(new[] { mentor }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Mentors_Update([DataSourceRequest]DataSourceRequest request, Mentor mentor)
        {
            if (ModelState.IsValid)
            {
                var entity = new Mentor
                {
                    M_Id = mentor.M_Id,
                    M_Name = mentor.M_Name
                };

                db.Mentors.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { mentor }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Mentors_Destroy([DataSourceRequest]DataSourceRequest request, Mentor mentor)
        {
            if (ModelState.IsValid)
            {
                var entity = new Mentor
                {
                    M_Id = mentor.M_Id,
                    M_Name = mentor.M_Name
                };

                db.Mentors.Attach(entity);
                db.Mentors.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { mentor }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
