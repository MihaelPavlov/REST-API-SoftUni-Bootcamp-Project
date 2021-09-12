namespace BootcampExamProjectMVC.Services.JobsServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BootcampExamProjectMVC.InputModels;
    using BootcampExamProjectMVC.Models;

    public interface IJobsService
    {
        Job GetJobById(string id);

        IEnumerable<Job> GetAllJobsWithThatSkillName(string name);

        Job CreateJob(InputModelJob input);

        bool DeleteJobById(string id);

        void CreateAvailableInterviews(Dictionary<string, string> suitableCandidates);

        List<KeyValuePair<string, string>> CheckIfWeHaveSuitableCandidatesForJobs();
    }
}
