using BootcampExamProjectMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampExamProjectMVC
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
              : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Recruiter> Recruiters { get; set; }

        public DbSet<Interview> Interviews { get; set; }

        public DbSet<JobSkill> JobSkills { get; set; }

        public DbSet<CandidateSkill> CandidateSkills { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Interview>().HasKey(i => new { i.CandidateId, i.JobId });

            builder.Entity<JobSkill>(entity =>
            {
                entity.HasKey(x => new { x.JobId, x.SkillId });
            });

            builder.Entity<CandidateSkill>(entity =>
            {
                entity.HasKey(x => new { x.CandidateId, x.SkillId });
            });

        }
    }
}
