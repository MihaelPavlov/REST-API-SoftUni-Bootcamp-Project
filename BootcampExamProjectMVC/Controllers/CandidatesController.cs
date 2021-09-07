using BootcampExamProjectMVC.InputModels;
using BootcampExamProjectMVC.Models;
using BootcampExamProjectMVC.Services.CandidatesServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BootcampExamProjectMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ILogger<CandidatesController> _logger;
        private readonly ICandidatesService candidatesService;

        // TODO: Move all Logic to Services
        public CandidatesController(ILogger<CandidatesController> logger, ICandidatesService candidatesService)
        {
            this._logger = logger;
            this.candidatesService = candidatesService;
        }

        [HttpGet]
        public IEnumerable<Candidate> Get()
        {
            return this.candidatesService.GetAllCandidates();
        }

        [HttpGet("{id}")]
        public Candidate Get(string id)
        {
            var mapCandidate = this.candidatesService.GetCandidateById(id);
            mapCandidate.CandidateSkills = this.candidatesService.GetCandidateSkillsById(id).ToList();
            return mapCandidate;
        }

        [HttpPost]
        public Candidate Post(InputModelCandidate input)
        {

            var candidate = this.candidatesService.CreateCandidate(input);
            return candidate;
        }


        [HttpPut("{id}")]
        public Candidate Put(string id, InputModelCandidate input)
        {
            var candidate = this.candidatesService.UpdateCandidate(id, input);
            return candidate;
        }


        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            var isCandidateDeleted = this.candidatesService.DeleteCandidateById(id);


            if (!isCandidateDeleted)
            {
                return "Candidate Doesn't Exist!";

            }

            return "Candidate Is Deleted Successfully!";
        }


    }
}
