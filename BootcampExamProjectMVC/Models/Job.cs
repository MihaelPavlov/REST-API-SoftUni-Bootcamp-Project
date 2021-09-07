using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampExamProjectMVC.Models
{
    public class Job
    {

        public Job()
        {
            this.Id = Guid.NewGuid().ToString();
            this.JobSkills = new HashSet<JobSkill>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Salary { get; set; }

        public ICollection<JobSkill> JobSkills { get; set; }
    }
}
