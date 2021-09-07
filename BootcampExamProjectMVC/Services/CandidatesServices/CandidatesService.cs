namespace BootcampExamProjectMVC.Services.CandidatesServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BootcampExamProjectMVC.InputModels;
    using BootcampExamProjectMVC.Models;

    public class CandidatesService : ICandidatesService
    {
        private readonly ApplicationDbContext db;

        public CandidatesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        private Recruiter CreateRecruiter(InputModelRecruiter input)
        {
            var recruiter = new Recruiter
            {
                LastName = input.LastName,
                Email = input.Email,
                Country = input.Country,
            };
            this.db.Recruiters.Add(recruiter);
            return recruiter;
        }

        private void CreateNewSkills(ICollection<InputModelSkill> inputSkills)
        {
            var newSkills = new List<Skill>(inputSkills.Count);

            var skillInDatabase = this.db.Skills.ToList();

            foreach (var skill in inputSkills)
            {
                if (!skillInDatabase.Any(x => x.Name == skill.Name))
                {
                    newSkills.Add(new Skill { Name = skill.Name });
                }
            }

            this.db.Skills.AddRange(newSkills);
            this.db.SaveChanges();

        }
        public void RemoveCandidateSkills(string candidateId)
        {
            var candidateSkills = this.db.CandidateSkills.Where(x => x.CandidateId == candidateId).ToList();
            this.db.RemoveRange(candidateSkills);
            this.db.SaveChanges();
        }
        public IEnumerable<CandidateSkill> CreateCandidateSkills(string candidateId, ICollection<InputModelSkill> inputSkills)
        {
            var candidateSkills = new List<CandidateSkill>(inputSkills.Count);
            var newCandidateSkills = new List<CandidateSkill>();

            foreach (var skill in inputSkills)
            {
                var skillFromTheDB = this.db.Skills.FirstOrDefault(x => x.Name == skill.Name);


                candidateSkills.Add(new CandidateSkill { CandidateId = candidateId, SkillId = skillFromTheDB.Id, Skill = skillFromTheDB });
            }

            this.db.CandidateSkills.AddRange(candidateSkills);
            this.db.SaveChanges();
            return candidateSkills;
        }

        public Candidate CreateCandidate(InputModelCandidate input)
        {
            var newCandidate = new Candidate
            {
                Description = input.Description,
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
            };

            //DONE: Logic recruiter
            var recruiter = this.db.Recruiters.FirstOrDefault(x => x.LastName == input.Recruiter.LastName);
            if (recruiter == null)
            {//if go here it means that the recruiter is null and we assign new recruiter to the variable 
                recruiter = CreateRecruiter(input.Recruiter);
            }
            else
            {//if reactruiter exist just up exp with one
                recruiter.ExperienceLevel += 1;
            }

            newCandidate.RecruiterId = recruiter.Id;


            //Done: Logic to add newSkills and candidateSkills
            this.CreateNewSkills(input.Skills);
            newCandidate.CandidateSkills = this.CreateCandidateSkills(newCandidate.Id, input.Skills).ToList();


            this.db.Candidates.Add(newCandidate);

            this.db.SaveChanges();
            return newCandidate;
        }

        public bool DeleteCandidateById(string candidateId)
        {
            var candidateToDelete = this.db.Candidates.FirstOrDefault(x => x.Id == candidateId);
            if (candidateToDelete == null)
            {
                return false;
            }
            var removeCandidateSkill = this.db.CandidateSkills.Where(x => x.CandidateId == candidateToDelete.Id).ToList();

            this.db.CandidateSkills.RemoveRange(removeCandidateSkill);
            this.db.Candidates.Remove(candidateToDelete);
            this.db.SaveChanges();
            return true;
        }

        public IEnumerable<Candidate> GetAllCandidates()
        {
            var allCandidates = this.db.Candidates.ToList();
            return allCandidates;
        }

        public Candidate GetCandidateById(string candidateId)
        {
            var candidateById = this.db.Candidates.FirstOrDefault(x => x.Id == candidateId);

            return candidateById;
        }

        public Recruiter GetCandidateRecruiterById(string candidateRecruiterId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CandidateSkill> GetCandidateSkillsById(string candidateId)
        {
            var candidateSkills = this.db.CandidateSkills.Where(x => x.CandidateId == candidateId).ToList();
            return candidateSkills;
        }

        public Candidate UpdateCandidate(string id, InputModelCandidate input)
        {
            var alreadyCreated = this.db.Candidates.FirstOrDefault(x => x.Id == id);

            if (alreadyCreated == null)
            {
                return null;
            }
            alreadyCreated.Description = input.Description;
            alreadyCreated.Email = input.Email;
            alreadyCreated.FirstName = input.FirstName;
            alreadyCreated.LastName = input.LastName;

            //DONE: Logic check is this recruiter already created
            var recruiter = this.db.Recruiters.FirstOrDefault(x => x.LastName == input.Recruiter.LastName);
            if (recruiter == null)
            {
                this.CreateRecruiter(input.Recruiter);
            }
            else
            {
                recruiter.ExperienceLevel += 1;
            }

            alreadyCreated.RecruiterId = recruiter.Id;



            //Done: Logic to add newSkills and candidateSkills
            this.CreateNewSkills(input.Skills);
            this.RemoveCandidateSkills(alreadyCreated.Id);
            alreadyCreated.CandidateSkills = this.CreateCandidateSkills(alreadyCreated.Id, input.Skills).ToList();


            this.db.Candidates.Update(alreadyCreated);

            this.db.SaveChanges();
            return alreadyCreated;
        }

        public bool IsFirstNameAvailable(string firstName)
        {
            return this.db.Candidates.Any(x => x.FirstName == firstName);
        }

        public bool IsLastNameAvailable(string lastName)
        {
            return this.db.Candidates.Any(x => x.LastName == lastName);
        }

        public bool IsEmailAvailable(string email)
        {
            return this.db.Candidates.Any(x => x.Email == email);
        }
    }
}
