using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Naukri.Models;

namespace Naukri.Models
{
    public class MyDBContext :DbContext
    {
        public MyDBContext():base("NaukriDatabase")
        {

        }

        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Naukri.Models.AppliedJob> AppliedJobs { get; set; }
    }
}