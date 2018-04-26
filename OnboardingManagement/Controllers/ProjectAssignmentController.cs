using Kendo.Mvc.Extensions;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

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
                return View("Index");

            }
            catch (Exception e1)
            {
                ViewBag.msg = "Assignment of same project to onboarder";
                return View("Index");
                //return JavaScript("window.alert('Assignment of same project to onboarder');location.reload();"); //return JavaScript("<script>alert(\"some message\")</script>");
            }
        }
        public void PopulateForeignKeys()
        {
            ViewData["Projects"] = db.Projects;
            ViewData["Onboarders"] = db.Onboarders;
            ViewData["Mentors"] = db.Mentors;
        }

        public ActionResult DisplayAllAssignments()
        {
            PopulateForeignKeys();
            return View();
        }

        public ActionResult ProjectAssignments_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<ProjectAssignment> projectassignemts = db.ProjectAssignments;
            DataSourceResult result = projectassignemts.ToDataSourceResult(request, projectassignemt => new
            {
                O_Id = projectassignemt.O_Id,
                P_Id = projectassignemt.P_Id,
                M_Id = projectassignemt.M_Id,
                PA_Rotation_Num = projectassignemt.PA_Rotation_Num,
                PA_Start_Date = projectassignemt.PA_Start_Date,
                PA_End_Date = projectassignemt.PA_End_Date
            });

            return Json(result);
        }
    }
}