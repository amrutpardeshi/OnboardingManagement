using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnboardingManagement.Models
{
    public class Onboarder
    {
        [Key]
        public int O_Id { get; set; }
        public string O_Name { get; set; }
        public int O_Rotation_Num { get; set; }
        public int RM_Id { get; set; }

        //Foreign Keys
        public ICollection<ProjectAssignment> ProjectAssignments { get; set; }
    }
}