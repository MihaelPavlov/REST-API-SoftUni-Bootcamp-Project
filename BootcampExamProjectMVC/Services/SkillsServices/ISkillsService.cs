namespace BootcampExamProjectMVC.Services.SkillsServices
{
    using BootcampExamProjectMVC.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ISkillsService
    {
        string GetSkillsWhichHaveCandidates();

        Skill GetSkillById(string skillId);

        Skill GetSkillByName(string name);

        IEnumerable<Skill> GetAllSkills();
    }
}
