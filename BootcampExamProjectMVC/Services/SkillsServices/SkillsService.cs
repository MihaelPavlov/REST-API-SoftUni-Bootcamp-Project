namespace BootcampExamProjectMVC.Services.SkillsServices
{
    using BootcampExamProjectMVC.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SkillsService : ISkillsService
    {
        private readonly ApplicationDbContext db;

        public SkillsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            return this.db.Skills.ToList();
        }

        public Skill GetSkillById(string skillId)
        {
            var skill = this.db.Skills.FirstOrDefault(x => x.Id == skillId);
            return skill;
        }

        public Skill GetSkillByName(string name)
        {
            var skill = this.db.Skills.FirstOrDefault(x => x.Name == name);
            return skill;
        }

        public string GetSkillsWhichHaveCandidates()
        {
            var candidateSkills = this.db.CandidateSkills.ToList();
            var skills = this.db.Skills.ToList();
            var activeSkills = new HashSet<string>();
            foreach (var candidateSkill in candidateSkills)
            {
                var getSkills = skills.FirstOrDefault(x => x.Id == candidateSkill.SkillId);
                if (!activeSkills.Contains(getSkills.Name))
                {
                    activeSkills.Add(getSkills.Name);
                }
            }
            return string.Join(" ", activeSkills);
        }
    }
}
