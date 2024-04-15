using DemoApp1.ENTITY;
using DemoApp1.INTERFACE;
using Microsoft.EntityFrameworkCore;

namespace DemoApp1.SERVICE
{
    public class ResumeService : Repository<ResumeEntity>, IResumeService
    {
        public ApplicationDbContext _dbContext;


        public ResumeService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResumeEntity> GetResumeByIdAsync(int resumeId)
        {
            // Create an empty instance of ResumeEntity
            var data = new ResumeEntity();

            // Fetch the specific resume and its associated details by ID
            var queryResult = await _dbContext.Resumes
                .Where(r => r.Id == resumeId)
                .Join(_dbContext.EducationDetails,
                    resume => resume.Id,
                    education => education.ResumeId,
                    (resume, education) => new { Resume = resume, EducationDetails = education })
                .Join(_dbContext.WorkExperiences,
                    combined => combined.Resume.Id,
                    work => work.ResumeId,
                    (combined, work) => new { combined.Resume, combined.EducationDetails, WorkExperience = work })
                .Join(_dbContext.LanguageProficiencies,
                    combined => combined.Resume.Id,
                    language => language.ResumeId,
                    (combined, language) => new { combined.Resume, combined.EducationDetails, combined.WorkExperience, KnownLanguage = language })
                .Join(_dbContext.TechnicalExperiences,
                    combined => combined.Resume.Id,
                    technical => technical.ResumeId,
                    (combined, technical) => new { combined.Resume, combined.EducationDetails, combined.WorkExperience, combined.KnownLanguage, TechnicalExperience = technical })
                .Join(_dbContext.Preferences, // Use Preferences instead of Preference
                    combined => combined.Resume.Id,
                    preference => preference.ResumeId,
                    (combined, preference) => new { combined.Resume, combined.EducationDetails, combined.WorkExperience, combined.KnownLanguage, combined.TechnicalExperience, Preference = preference })
                .FirstOrDefaultAsync();

            // If a record is found, assign the result to EmptyData, otherwise, keep it as an empty ResumeEntity
            if (queryResult != null)
            {
                data = queryResult.Resume;
            }
            return data;

        }

    }
}
