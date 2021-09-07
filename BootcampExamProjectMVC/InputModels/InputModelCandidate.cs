using BootcampExamProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampExamProjectMVC.InputModels
{
    public class InputModelCandidate
    {
        public InputModelCandidate()
        {
            this.Skills = new HashSet<InputModelSkill>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public InputModelRecruiter Recruiter { get; set; }
        public  ICollection<InputModelSkill> Skills { get; set; }
    }
}
