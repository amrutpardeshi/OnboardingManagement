﻿using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using OnboardingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnboardingManagement.Controllers
{
   
    public class ProjectAssignmentController : Controller
    {
        private OnboardingManagementDb db = new OnboardingManagementDb();
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            PopulateForeignKeys();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ProjectAssignment projectAssignments)
        {
           try
            {
                var entity = new ProjectAssignment
                {
                    O_Id = projectAssignments.O_Id,
                    P_Id = projectAssignments.P_Id,
                    M_Id = projectAssignments.M_Id,
                    PA_Rotation_Num = projectAssignments.PA_Rotation_Num,
                    PA_Start_Date = projectAssignments.PA_Start_Date,
                    PA_End_Date = projectAssignments.PA_End_Date
                };
                db.ProjectAssignments.Add(entity);
                db.SaveChanges();
                projectAssignments.PA_Id = entity.PA_Id;
                ViewBag.msg = "Assigned";
                return View("Create");

            }
            catch (Exception e1)
            {
                ViewBag.msg = "Assignment of same project to onboarder";
                return View("Create");
            }
        }

        [HttpGet]
      public  JsonResult GetProjectSuggestion(int id)
        {
            var pid = (from onboarder in db.Onboarders
                       join project in db.ProjectAssignments
                       on onboarder.O_Id equals project.O_Id
                       where project.O_Id == id
                       select project.P_Id
                         ).ToList();  //Get The list of Project Id on which onboarder already worked

            List<Project> projects = db.Projects.Where(t => pid.Contains(t.P_Id)).ToList();
            List<String> projectTechnology = projects.Select(p => p.P_Technology).ToList();
            List<Project> result = db.Projects.Where(t => !pid.Contains(t.P_Id) && !projectTechnology.Contains(t.P_Technology)).ToList();
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public void PopulateForeignKeys()
        {
            ViewData["Projects"] = db.Projects;
            ViewData["Onboarders"] = db.Onboarders;
            ViewData["Mentors"] = db.Mentors;
        }

        public ActionResult ProjectAssignments_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<ProjectAssignment> projectassignemts = db.ProjectAssignments;
            DataSourceResult result = projectassignemts.ToDataSourceResult(request, projectassignemt => new
            {
                PA_Id = projectassignemt.PA_Id,
                O_Id = projectassignemt.O_Id,
                P_Id = projectassignemt.P_Id,
                M_Id = projectassignemt.M_Id,
                PA_Rotation_Num = projectassignemt.PA_Rotation_Num,
                PA_Start_Date = projectassignemt.PA_Start_Date,
                PA_End_Date = projectassignemt.PA_End_Date
            });

            return Json(result);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
             try
            {
                ProjectAssignment record = db.ProjectAssignments.FirstOrDefault(m => m.PA_Id == id);
                db.ProjectAssignments.Remove(record);
                db.SaveChanges();
                return JavaScript("<script>alert('Record Deleted')</script>");
                //return View("Delete");
            }
            catch (Exception e)
            {
            }
            return View("Delete");
        }

        [HttpPost]
        public JsonResult ProjectAssignment_Delete_Fetch(string o_id,string rotation_no)
        {
            try
            {
                int id1 = Int32.Parse(o_id), rotation = Int32.Parse(rotation_no);
                ProjectAssignment projectAssignmet = db.ProjectAssignments.FirstOrDefault(m => m.O_Id == id1 && m.PA_Rotation_Num == rotation);
                if (projectAssignmet != null)
                {
                    TempProjectAssignment tpa = new TempProjectAssignment
                    {
                        PA_Id = id1,
                        Project = db.Projects.Find(projectAssignmet.P_Id),
                        Onboarder = db.Onboarders.Find(projectAssignmet.O_Id),
                        Mentor = db.Mentors.Find(projectAssignmet.M_Id),
                        Psn = projectAssignmet
                    };
                    return Json(tpa, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e) {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRotationNo(int id)
        {
            var R_No = 0;
            var R_No1 = (from onboarder in db.Onboarders
                        join project in db.ProjectAssignments
                        on onboarder.O_Id equals project.O_Id
                        where project.O_Id == id
                        select project.PA_Rotation_Num
                         ).ToList();

            var reuslt=db.ProjectAssignments.Where(t => t.O_Id == id).ToList();
            if (reuslt.Count!=0)
            {
              R_No =  reuslt.Max(r => r.PA_Rotation_Num);
                R_No++;

            }
            else
            {
                R_No = 1 ;
            }
            return Json(R_No,JsonRequestBehavior.AllowGet);
        }
        
    }
    
    class TempProjectAssignment
    {
        public int PA_Id { get; set; }
        public Project Project;
        public Onboarder Onboarder;
        public Mentor Mentor;
        public ProjectAssignment Psn;
    };
}
