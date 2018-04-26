using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnboardingManagement.Models
{
    public class OnboardingManagementDb : DbContext
    {
        public OnboardingManagementDb() : base("DefaultConnection")
        {

        }

        public DbSet<ReportingManager> ReportingManagers { get; set; }

    
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Onboarder> Onboarders { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectAssignment> ProjectAssignments { get; set; }
        public DbSet<User> Users { get; set; }
       
        public DbSet<Login_user> Login { get; set; }

    }
}