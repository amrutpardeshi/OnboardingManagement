using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnboardingManagement.Models
{
    public class Login_user
    {
        [Key]
        public int U_Id { get; set; }

        [Required]
        public string U_Name { get; set; }

        [Required]
        public string U_Password { get; set; }
        public string U_Role { get; set; }
    }
}