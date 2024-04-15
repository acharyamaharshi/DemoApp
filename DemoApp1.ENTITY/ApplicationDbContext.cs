using DemoApp1.COMMON;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DemoApp1.COMMON.ENUMS;

namespace DemoApp1.ENTITY
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ResumeEntity> Resumes { get; set; }
        public DbSet<EducationDetailEntity> EducationDetails { get; set; }
        public DbSet<WorkExperienceEntity> WorkExperiences { get; set; }
        public DbSet<LanguageProficiencyEntity> LanguageProficiencies { get; set; }
        public DbSet<TechnicalExperienceEntity> TechnicalExperiences { get; set; }
        public DbSet<PreferenceEntity> Preferences { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity relationships here if needed
            ConfigureEntityRelationships(modelBuilder);

        }

        // Configure entity relationships
        private void ConfigureEntityRelationships(ModelBuilder modelBuilder)
        {
            // Add entity configurations here if needed
        }

        // Seed users
        public void SeedUsers()
        {
            // Check if there are already users in the database
            if (!Users.Any())
            {
                // Add some default users
                Users.Add(new UserEntity { UserName = "admin", Password = EncriptionService.EncryptPassword("Ganesha@123"), UserType = UserType.Admin });
                // Add more users as needed
                SaveChanges();
            }
        }
    }
}
