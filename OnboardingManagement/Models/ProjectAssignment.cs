using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnboardingManagement.Models
{
    public class ProjectAssignment
    {
        [Key]
        public int PA_Id { get; set; }

        [Index("UniqueConstrain",1, IsUnique = true)]
        public int O_Id { get; set; }

        [Index("UniqueConstrain", 2, IsUnique = true)]
        public int P_Id { get; set; }

        [Index("UniqueConstrain",3, IsUnique = true)]
        public int M_Id { get; set; }

        [Index("UniqueConstrain", 4, IsUnique = true)]
        public int PA_Rotation_Num { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PA_Start_Date { get; set; }


        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PA_End_Date { get; set; }
    }
}