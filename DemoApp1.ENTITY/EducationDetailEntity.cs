using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp1.ENTITY
{
    public class EducationDetailEntity : DB_COMMONFIELDS
    {
        public int Id { get; set; }
        public int ResumeId { get; set; } // Foreign key referencing Resume

        public string Degree { get; set; }
        public double Marks { get; set; }
        public int PassingYear { get; set; }
        public double CGPA { get; set; }
        public string BoardName { get; set; }

        [ForeignKey("ResumeId")]
        public ResumeEntity Resume { get; set; } // Navigation property

    }
}
