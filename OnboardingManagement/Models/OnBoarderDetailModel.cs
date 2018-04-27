using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnboardingManagement.Models
{
    public class OnBoarderDetailModel
    {
        public int OnboarderId { get; set; }
        public String Name { get; set; }
        public String Rotation1 { get; set; } = "---";
        public String Rotation2 { get; set; } = "---";
        public String Rotation3 { get; set; } = "---";
        public String Rotation4 { get; set; } = "---";
    }
}