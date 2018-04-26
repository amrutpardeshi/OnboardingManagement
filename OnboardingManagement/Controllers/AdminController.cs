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
        public ActionResult MentorView()
        {

            return View();
        }

    }
}