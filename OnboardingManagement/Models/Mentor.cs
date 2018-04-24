using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnboardingManagement.Models
{
    public class Mentor
    {
        [Key]
        public int M_Id { get; set; }
        public string M_Name { get; set; }

        //Foreign Keys
        public ICollection<ProjectAssignment> ProjectAssignments { get; set; }

    }
}