using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnboardingManagement.Models
{
    public class Project : IEquatable<Project>
    {
        [Key]
        public int P_Id { get; set; }


     //   [Index("UniqueConstrain", 2, IsUnique = true)]
        [Required]
        public string P_Name { get; set; }
       // [Index("UniqueConstrain", 2, IsUnique = true)]
        [Required]
        public string P_Technology { get; set; }

        //Foreign Keys
        public ICollection<ProjectAssignment> ProjectAssignments { get; set; }

        public bool Equals(Project other)
        {
            if (this.P_Id == other.P_Id && this.P_Technology.Equals(other.P_Technology))
                return true;
            else
                return false;
        }
    }
}