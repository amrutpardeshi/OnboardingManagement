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
        public ActionResult Index()
        {
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

        [Authorize]
        public ActionResult MentorView(int id)
        {


            DateTime today = DateTime.Now;

           List<ProjectAssignment> projects= db.ProjectAssignments
                                                .Where(m => m.PA_Start_Date < today && m.PA_End_Date > today && m.M_Id == id).ToList(); //Get All Onboarder



            List<OnBoarderDetailModel> onboarderslist = new List<OnBoarderDetailModel>();
         
            foreach (ProjectAssignment project in projects)
            {
                OnBoarderDetailModel model = new OnBoarderDetailModel();

                List<ProjectAssignment> onboarderProject = db.ProjectAssignments.Where(m => m.O_Id == project.O_Id).ToList();//get all projects of onboarder
                
                
                     
                
              


            }


            return View();
        }

    }
}