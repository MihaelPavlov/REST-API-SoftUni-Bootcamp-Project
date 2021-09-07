namespace BootcampExamProjectMVC.Services.RecruitersServices
{
    using BootcampExamProjectMVC.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RecruitersService : IRecruitersService
    {
        private readonly ApplicationDbContext db;

        public RecruitersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Recruiter> GetAllRecruites()
        {
            var allRecruiters = this.db.Recruiters.ToList();
            var recruitersWithCandidateSkills = this.GetRecruitersListAllCandidateSkills(allRecruiters);
            return recruitersWithCandidateSkills;
        }

        public IEnumerable<Recruiter> GetRecruitersByLevel(int level)
        {
            var recruiters = this.db.Recruiters.Where(x => x.ExperienceLevel == level).ToList();
            var recruitersWithCandidateSkills = this.GetRecruitersListAllCandidateSkills(recruiters);
            return recruitersWithCandidateSkills;
        }

        private IEnumerable<Recruiter> GetRecruitersListAllCandidateSkills(List<Recruiter> recruiters)
        {
            foreach (var recruiter in recruiters)
            {
                recruiter.Candidates = this.db.Candidates.Where(x => x.RecruiterId == recruiter.Id).ToList();
                foreach (var candidate in recruiter.Candidates)
                {
                    candidate.CandidateSkills = this.db.CandidateSkills.Where(x => x.CandidateId == candidate.Id).ToList();

                    foreach (var candidateSkill in candidate.CandidateSkills)
                    {
                        candidateSkill.Skill = this.db.Skills.FirstOrDefault(x => x.Id == candidateSkill.SkillId);
                    }
                }
            }

            return recruiters;
        }
    }
}
