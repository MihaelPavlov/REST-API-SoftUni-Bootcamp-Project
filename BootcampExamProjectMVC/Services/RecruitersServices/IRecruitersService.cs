namespace BootcampExamProjectMVC.Services.RecruitersServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BootcampExamProjectMVC.Models;

    public interface IRecruitersService
    {
        IEnumerable<Recruiter> GetAllRecruites();

        IEnumerable<Recruiter> GetRecruitersByLevel(int level);
    }
}
