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


        public ActionResult ProjectAssignment_Destroy([DataSourceRequest]DataSourceRequest request, ProjectAssignment projectAssignment)
        {
           
                var entity = new ProjectAssignment
                {
                    PA_Id = projectAssignment.PA_Id,
                    O_Id = projectAssignment.O_Id,
                    P_Id= projectAssignment.P_Id,
                    M_Id = projectAssignment.M_Id,
                    PA_Rotation_Num = projectAssignment.PA_Rotation_Num,
                    PA_Start_Date = projectAssignment.PA_Start_Date,
                    PA_End_Date = projectAssignment.PA_End_Date

                };

                db.ProjectAssignments.Attach(entity);
                db.ProjectAssignments.Remove(entity);
                db.SaveChanges();
            

            return Json(new[] { projectAssignment }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult ProjectAssignment_Delete()
        {
            return View();
        }

        [HttpPost]
        public void ProjectAssignment_Delete(int id)
        {

        }

        [HttpPost]
        public JsonResult ProjectAssignment_Delete_Fetch(string o_id,string rotation_no)
        {
            int id1 = Int32.Parse(o_id), rotation = Int32.Parse(rotation_no);
            ProjectAssignment projectAssignmet = db.ProjectAssignments.FirstOrDefault(m => m.O_Id == id1 &&  m.PA_Rotation_Num == rotation);
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
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
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
