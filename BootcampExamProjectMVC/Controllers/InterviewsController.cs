using BootcampExamProjectMVC.Services.InterviewsServices;
using BootcampExamProjectMVC.Services.JobsServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampExamProjectMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterviewsController : Controller
    {
        private readonly IInterviewsService interviewsService;
        private readonly IJobsService jobsService;


        // TODO: Move all Logic to Services
        public InterviewsController(IInterviewsService interviewsService, IJobsService jobsService)
        {
            this.interviewsService = interviewsService;
            this.jobsService = jobsService;
        }

        [HttpGet]
        public List<KeyValuePair<string, string>> Get()
        {
            var result = this.jobsService.CheckIfWeHaveSuitableCandidatesForJobs();
            return result;
        }
    }
}
