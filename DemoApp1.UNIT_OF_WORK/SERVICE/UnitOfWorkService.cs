using DemoApp1.ENTITY;
using DemoApp1.INTERFACE;
using DemoApp1.SERVICE;
using DemoApp1.UNIT_OF_WORK.INTERFACE;


namespace DemoApp1.UNIT_OF_WORK.SERVICE
{
    public class UnitOfWorkService: IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public int Id { get; set; }

        public UnitOfWorkService(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;

            // initializing Repositories in Every UOW Instance

            UserRepo = new Repository<UserEntity>(_dbContext);
            ResumeRepo = new Repository<ResumeEntity>(_dbContext);
            EducationDetailRepo = new Repository<EducationDetailEntity>(_dbContext);
            WorkExperienceRepo = new Repository<WorkExperienceEntity>(_dbContext);
            LanguageProficiencyRepo = new Repository<LanguageProficiencyEntity>(_dbContext);
            TechnicalExperienceRepo = new Repository<TechnicalExperienceEntity>(_dbContext);
            PreferenceRepo = new Repository<PreferenceEntity>(_dbContext);

        }



        // Register Project Repositories in UOW Bellow...
        public IRepository<UserEntity> UserRepo { get; private set; }
        public IRepository<ResumeEntity> ResumeRepo { get; private set; }

        public IRepository<EducationDetailEntity> EducationDetailRepo { get; private set; }

        public IRepository<WorkExperienceEntity> WorkExperienceRepo { get; private set; }

        public IRepository<LanguageProficiencyEntity> LanguageProficiencyRepo { get; private set; }
        public IRepository<TechnicalExperienceEntity> TechnicalExperienceRepo { get; private set; }

        public IRepository<PreferenceEntity> PreferenceRepo { get; }

        // Added Common DB Methods that will be Accacible Across all The Repose 
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
