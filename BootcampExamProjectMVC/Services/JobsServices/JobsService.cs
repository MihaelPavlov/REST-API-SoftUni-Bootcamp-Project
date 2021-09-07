namespace BootcampExamProjectMVC.Services.JobsServices
{
    using BootcampExamProjectMVC.InputModels;
    using BootcampExamProjectMVC.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class JobsService : IJobsService
    {
        private readonly ApplicationDbContext db;

        public JobsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Job CreateJob(InputModelJob input)
        {
            var job = new Job
            {
                Description = input.Description,
                Title = input.Title,
                Salary = input.Salary
            };

            var newSkills = new List<Skill>(input.JobSkills.Count);

            var jobSkills = new List<JobSkill>(input.JobSkills.Count);

            var skillInDatabase = this.db.Skills.ToList();

            foreach (var skill in input.JobSkills)
            {
                if (!skillInDatabase.Any(x => x.Name == skill.Name))
                {
                    newSkills.Add(new Skill { Name = skill.Name });
                }
            }

            this.db.Skills.AddRange(newSkills);
            this.db.SaveChanges();

            foreach (var skill in input.JobSkills)
            {
                var skillFromTheDB = this.db.Skills.FirstOrDefault(x => x.Name == skill.Name);
                jobSkills.Add(new JobSkill { JobId = job.Id, SkillId = skillFromTheDB.Id });
            }


            job.JobSkills = jobSkills;
            this.db.JobSkills.AddRange(jobSkills);
            this.db.Jobs.Add(job);
            this.db.SaveChanges();
            return job;
        }

        public bool DeleteJobById(string id)
        {
            var jobToDelete = this.db.Jobs.FirstOrDefault(x => x.Id == id);
            if (jobToDelete == null)
            {
                return false;
            }
            var removeJobSkill = this.db.JobSkills.Where(x => x.JobId == jobToDelete.Id).ToList();

            this.db.JobSkills.RemoveRange(removeJobSkill);
            this.db.Jobs.Remove(jobToDelete);
            this.db.SaveChanges();
            return true;
        }

        public IEnumerable<Job> GetAllJobsWithThatSkillName(string name)
        {
            var allJobs = this.db.Jobs.ToList();
            var allJobSkills = this.db.JobSkills.ToList();

            foreach (var jobS in allJobSkills)
            {
                foreach (var j in allJobs)
                {
                    if (j.Id == jobS.JobId)
                    {
                        j.JobSkills.Add(jobS);
                    }
                }
            }
            var jobsWithThatSkillName = new List<Job>();
            foreach (var job in allJobs)
            {
                foreach (var jobSkill in job.JobSkills)
                {
                    var skill = this.db.Skills.FirstOrDefault(x => x.Name == name);
                    if (jobSkill.SkillId == skill.Id)
                    {
                        jobsWithThatSkillName.Add(job);
                    }
                }
            }
            return jobsWithThatSkillName;
        }

        public Job GetJobById(string id)
        {
            return this.db.Jobs.FirstOrDefault(x => x.Id == id);
        }
    }
}
