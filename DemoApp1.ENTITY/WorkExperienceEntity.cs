using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp1.ENTITY
{
    public class WorkExperienceEntity : DB_COMMONFIELDS
    {
        public int Id { get; set; }
        public int ResumeId { get; set; } // Foreign key referencing Resume
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }


        [ForeignKey("ResumeId")]
        public ResumeEntity Resume { get; set; } // Navigation property
    }
}
