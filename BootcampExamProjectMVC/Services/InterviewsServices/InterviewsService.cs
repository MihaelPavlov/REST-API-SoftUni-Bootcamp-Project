namespace BootcampExamProjectMVC.Services.InterviewsServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InterviewsService :IInterviewsService
    {
        private readonly ApplicationDbContext db;

        public InterviewsService(ApplicationDbContext db)
        {
            this.db = db;
        }

    }
}
