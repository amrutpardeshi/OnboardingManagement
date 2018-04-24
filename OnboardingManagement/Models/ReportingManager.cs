using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnboardingManagement.Models
{
    public class ReportingManager
    {
        [Key]
        public int RM_Id { get; set; }
        public string RM_Name { get; set; }

        //Foreign Keys
        public ICollection<Onboarder> Onboarders { get; set; }
    }
}