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
        public ActionResult Index(int id)
        {
            var pid = (from onboarder in db.Onboarders
                            join project in db.ProjectAssignments
                            on onboarder.O_Id equals project.O_Id
                            where project.O_Id == id
                            select project.P_Id
                         ).ToList();  //Get The list of Project Id on which onboarder already worked

            List<Project> projects = db.Projects.Where(t => pid.Contains(t.P_Id)).ToList(); 
            List<String> projectTechnology = projects.Select(p=> p.P_Technology).ToList();
            List<Project> result =db.Projects.Where(t=> !pid.Contains(t.P_Id) && !projectTechnology.Contains(t.P_Technology)).ToList();
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