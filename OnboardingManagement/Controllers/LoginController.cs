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
            Login_user usr = Validate(user);
            if (usr!= null){
                FormsAuthentication.SetAuthCookie(user.U_Name, false);
                string role =usr.U_Role;
                if (role.Equals("Mentor"))
                {
                    return RedirectToAction("MentorView", "Admin",new { id=usr.U_Id});
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

       
        }
      
        private Login_user Validate(Login_user user) {
            Login_user result = db.Login.ToList().FirstOrDefault(m => m.U_Name == user.U_Name && m.U_Password == user.U_Password);
            return result;
        }



        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Login");
        }

       
    }
}