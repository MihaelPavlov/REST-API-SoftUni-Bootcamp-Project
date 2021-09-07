using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampExamProjectMVC.InputModels
{
    public class InputModelJob
    {
        public InputModelJob()
        {
            this.JobSkills = new HashSet<InputModelSkill>();
        }


        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Salary { get; set; }

        public ICollection<InputModelSkill> JobSkills { get; set; }
    }
}
