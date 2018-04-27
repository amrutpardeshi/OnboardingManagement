using System;
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
    public class TestMentorController : Controller
    {
        OnboardingManagementDb db = new OnboardingManagementDb();
        // GET: TestMentor
        public ActionResult Index()
        {
          
            return View();
        }


        public ActionResult GetAllMentors([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Mentor> mentors = db.Mentors;
            DataSourceResult result = mentors.ToDataSourceResult(request, mentor => new {
                M_Id = mentor.M_Id,
                M_Name = mentor.M_Name
            });
            return Json(result);

        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Mentor mentor)
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
                try
                {
                    var User_Entity = new Login_user
                    {
                        M_Id=mentor.M_Id,
                        U_Name = mentor.M_Name,
                        U_Role = "Mentor",
                        U_Password = mentor.M_Name,
                    };
                    db.Login.Add(User_Entity);
                    db.SaveChanges();
                   
                }
                catch (Exception e)
                {
                    
                }
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}