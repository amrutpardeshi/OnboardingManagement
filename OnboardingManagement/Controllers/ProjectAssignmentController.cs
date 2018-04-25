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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ProjectAssignment projectAssignments)
        {
            try
            {
                if (ModelState.IsValid)
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
                }
                return View();

            }
            catch (Exception e1)
            {
                ViewBag.msg = "Assignment of same project to onboarder";
                return View("Index");
                //return JavaScript("window.alert('Assignment of same project to onboarder');location.reload();"); //return JavaScript("<script>alert(\"some message\")</script>");
            }
            

        }
    }
}