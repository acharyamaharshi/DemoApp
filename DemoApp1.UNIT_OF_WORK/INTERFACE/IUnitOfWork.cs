
using DemoApp1.ENTITY;
using DemoApp1.INTERFACE;

namespace DemoApp1.UNIT_OF_WORK.INTERFACE
{
    public interface IUnitOfWork : IDisposable
    {
        public int Id { get; set; }
        // ------  Registering Project Repositories Here ----------------------------------------------------


        // --------------------------------------------------------------------------------------------------

        public IRepository<UserEntity> UserRepo { get; }
        public IRepository<ResumeEntity> ResumeRepo { get; }
        public IRepository<EducationDetailEntity> EducationDetailRepo { get; }

        public IRepository<WorkExperienceEntity> WorkExperienceRepo { get; }

        public IRepository<LanguageProficiencyEntity> LanguageProficiencyRepo { get; }
        public IRepository<TechnicalExperienceEntity> TechnicalExperienceRepo { get; }

        public IRepository<PreferenceEntity> PreferenceRepo { get; }


        public void SaveChanges();



    }
}
