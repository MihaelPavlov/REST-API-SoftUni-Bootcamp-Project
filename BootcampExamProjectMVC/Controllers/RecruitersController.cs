using BootcampExamProjectMVC.Models;
using BootcampExamProjectMVC.Services.RecruitersServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampExamProjectMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecruitersController : ControllerBase
    {
        private readonly IRecruitersService recruitersService;

        public RecruitersController(IRecruitersService recruitersService)
        {
            this.recruitersService = recruitersService;
        }

        [Route("/Recruiters/All")]
        [HttpGet]
        public IEnumerable<Recruiter> Get()
        {
            var allRecruiters = this.recruitersService.GetAllRecruites();
            return allRecruiters;
        }

        [HttpGet]
        public IEnumerable<Recruiter> Get(int level)
        {
            if (level==0)
            {
                throw new ArgumentException("You need to enter level");
            }
            var rectuiter = this.recruitersService.GetRecruitersByLevel(level);
            return rectuiter;
        }
    }
}
