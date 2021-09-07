using BootcampExamProjectMVC.InputModels;
using BootcampExamProjectMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampExamProjectMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        // TODO: Move all Logic to Services
        public JobsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public Job Post(InputModelJob input)
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

        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            var jobToDelete = this.db.Jobs.FirstOrDefault(x => x.Id == id);
            var removeJobSkill = this.db.JobSkills.Where(x => x.JobId == jobToDelete.Id).ToList();

            this.db.JobSkills.RemoveRange(removeJobSkill);
            this.db.Jobs.Remove(jobToDelete);
            this.db.SaveChanges();
            return this.Ok().ToString();

        }
    }
}
