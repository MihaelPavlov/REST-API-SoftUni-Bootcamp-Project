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
        private readonly ApplicationDbContext db;

        // TODO: Move all Logic to Services
        public InterviewsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public string Get()
        {
            //TODO: Implementation
            return "";
        }
    }
}
