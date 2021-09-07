namespace BootcampExamProjectMVC.Services.CandidatesServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BootcampExamProjectMVC.InputModels;
    using BootcampExamProjectMVC.Models;

    public interface ICandidatesService
    {
        bool IsFirstNameAvailable(string firstName);

        bool IsLastNameAvailable(string lastName);

        bool IsEmailAvailable(string email);

        IEnumerable<Candidate> GetAllCandidates();

        Candidate GetCandidateById(string candidateId);

        IEnumerable<CandidateSkill> GetCandidateSkillsById(string candidateSkillId);

        Recruiter GetCandidateRecruiterById(string candidateRecruiterId);

        Candidate CreateCandidate(InputModelCandidate input);

        Candidate UpdateCandidate(string id, InputModelCandidate input);

        bool DeleteCandidateById(string candidateId);
    }
}
