using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampExamProjectMVC.Models
{
    public class JobSkill
    {
        public string SkillId { get; set; }
        public Skill Skill { get; set; }
        public string JobId { get; set; }
    }
}
