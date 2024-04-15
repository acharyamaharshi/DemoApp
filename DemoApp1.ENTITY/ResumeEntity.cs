using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp1.ENTITY
{
    public class ResumeEntity : DB_COMMONFIELDS
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Summary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<EducationDetailEntity> EducationDetails { get; set; }
        public List<WorkExperienceEntity> WorkExperiences { get; set; }
        public List<LanguageProficiencyEntity> KnownLanguages { get; set; }
        public List<TechnicalExperienceEntity> TechnicalExperiences { get; set; }
        public PreferenceEntity Preference { get; set; } // Added preference field
    }
}
