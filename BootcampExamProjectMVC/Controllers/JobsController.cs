namespace BootcampExamProjectMVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BootcampExamProjectMVC.InputModels;
    using BootcampExamProjectMVC.Models;
    using BootcampExamProjectMVC.Services.JobsServices;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobsService jobsService;

        public JobsController(IJobsService jobsService)
        {
            this.jobsService = jobsService;
        }

        [HttpGet("{id}")]
        public Job Get(string id)
        {
            var job = this.jobsService.GetJobById(id);
            return job;
        }

        [Route("/Jobs/All")]
        [HttpGet]
        public ActionResult<IEnumerable<Job>>  All(string name)
        {
            var jobs = this.jobsService.GetAllJobsWithThatSkillName(name).ToList();
            return jobs;
        }

        [HttpPost]
        public Job Post(InputModelJob input)
        {
            var newJob = this.jobsService.CreateJob(input);
            return newJob;
        }

        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            var isJobDeleted = this.jobsService.DeleteJobById(id);
            if (!isJobDeleted)
            {
                return $"We don't have job with this ID {id} ";
            }

            return "Job Successfully Deleted!";
        }
    }
}
