using OnboardingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnboardingManagement.Controllers
{
    public class LoginController : Controller
    {
        OnboardingManagementDb db = new OnboardingManagementDb();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login_user user)
        {
            if (IsValid(user))
            {
                FormsAuthentication.SetAuthCookie(user.U_Name, false);
                string data = db.Login.Where(x => x.U_Name == user.U_Name).FirstOrDefault().U_Role;
                if (data == "Mentor")
                {
                  return   RedirectToAction("MentorView", "Admin");
                }
                else 
                {
                  return  RedirectToAction("Index", "Admin");
                }
            }
            else
            {
               return  RedirectToAction("Login", "Login");
            }
        }
      
        private bool IsValid(Login_user user)
        {
           
            Login_user re = db.Login.ToList().FirstOrDefault(m => m.U_Name == user.U_Name && m.U_Password == user.U_Password);
            if (re != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Login");
        }

       
    }
}