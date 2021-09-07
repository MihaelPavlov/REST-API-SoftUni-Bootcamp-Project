using BootcampExamProjectMVC.Models;
using BootcampExamProjectMVC.Services.SkillsServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampExamProjectMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsService skillsService;

        // TODO: Move all Logic to Services
        public SkillsController(ISkillsService skillsService)
        {
            this.skillsService = skillsService;
        }

        [HttpGet("{id}")]
        public Skill Get(string id)
        {
            var skill = this.skillsService.GetSkillById(id);
            return skill;
        }

        [Route("/Skills/Name")]
        [HttpGet]
        public ActionResult<Skill > Name(string name)
        {
            var skill = this.skillsService.GetSkillByName(name);
            return skill;
        }

        [Route("/Skills/All")]
        [HttpGet]
        public ActionResult<IEnumerable<Skill>> All()
        {
            var skill = this.skillsService.GetAllSkills().ToList();
            return skill;
        }

        [Route("/Skills/Active")]
        [HttpGet]
        public string Get()
        {
            var result = this.skillsService.GetSkillsWhichHaveCandidates();

            return result;
        }
    }
}
