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
    public class AdminController : Controller
    {
        OnboardingManagementDb db = new OnboardingManagementDb();
        [Authorize(Roles ="Admin")]
        // GET: Home
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [Authorize(Roles = "Admin,Mentor")]
        public ActionResult MView()
        {

            return View();
        }


        public ActionResult testView()
        {

            return View();
        }
        public String GetProjectNameById(int ProjectId)
        {
            return db.Projects.FirstOrDefault(m => m.P_Id == ProjectId).P_Name;
        }

        public String GetOnboarderNameById(int OnboarderId)
        {
            return db.Onboarders.FirstOrDefault(m => m.O_Id == OnboarderId).O_Name;
        }

        public JsonResult GetOnboardersForMentor([DataSourceRequest]DataSourceRequest request, int id)
        {
            
            String MentorName = db.Mentors.FirstOrDefault(m => m.M_Id == id).M_Name;
            ViewBag.MentorName = MentorName;

            DateTime today = DateTime.Now;
            List<ProjectAssignment> projects = db.ProjectAssignments
                                                 .Where(m => m.PA_Start_Date < today && m.PA_End_Date > today && m.M_Id == id).ToList(); //Get All Onboarder
            List<OnBoarderDetailModel> onboarderslist = new List<OnBoarderDetailModel>();
            foreach (ProjectAssignment project in projects)
            {
                OnBoarderDetailModel model = new OnBoarderDetailModel();
                List<ProjectAssignment> onboarderProject = db.ProjectAssignments.Where(m => m.O_Id == project.O_Id).ToList();//get all projects of onboarder
                foreach (ProjectAssignment onboarderdetail in onboarderProject)
                {
                    model.OnboarderId = onboarderdetail.O_Id;
                    model.Name = GetOnboarderNameById(onboarderdetail.O_Id);
                    String projectName = GetProjectNameById(onboarderdetail.P_Id);
                    if (model.Rotation1.Equals("---"))
                    {
                        model.Rotation1 = projectName;
                    }
                    else if (model.Rotation2.Equals("---"))
                    {
                        model.Rotation2 = projectName;
                    }
                    else if (model.Rotation3.Equals("---"))
                    {
                        model.Rotation3 = projectName;
                    }
                    else if (model.Rotation4.Equals("---"))
                    {
                        model.Rotation4 = projectName;
                    }
                }
                onboarderslist.Add(model);
            }
            return Json(onboarderslist.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult MentorView()
        {
            


            return View();
        }

    }
}