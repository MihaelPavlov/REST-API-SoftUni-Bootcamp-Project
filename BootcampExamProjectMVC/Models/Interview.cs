using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampExamProjectMVC.Models
{
    public class Interview
    {
        public string CandidateId { get; set; }

        public Candidate Candidate { get; set; }

        public string JobId { get; set; }

        public Job Job { get; set; }
    }
}
