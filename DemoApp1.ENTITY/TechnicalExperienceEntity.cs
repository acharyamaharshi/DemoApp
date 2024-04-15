using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DemoApp1.COMMON.ENUMS;

namespace DemoApp1.ENTITY
{
    public class TechnicalExperienceEntity : DB_COMMONFIELDS
    {
        public int Id { get; set; }
        public int ResumeId { get; set; } // Foreign key referencing Resume

        public string Language { get; set; }
        public Proficiency ProficiencyLevel { get; set; }


        [ForeignKey("ResumeId")]
        public ResumeEntity Resume { get; set; } // Navigation property


    }
}
