using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Naukri.Models;

namespace Naukri.Controllers
{
    [AuthorizationFilter]
    public class CandidatesController : Controller
    {
        private MyDBContext _db ;
        private  string _currentUser;
        public CandidatesController()
        {
            _db =new MyDBContext();
        }
        public async Task<ActionResult> GetAllJobs()
        {
            if(System.Web.HttpContext.Current.Session["Role"].ToString()!="Candidate")
                return View("~/Views/Shared/NoAcsess.cshtml");

            _currentUser= System.Web.HttpContext.Current.Session["UserName"].ToString();
            var job = from c in _db.Jobs
                      where !(from o in _db.AppliedJobs
                              where o.Candidate.Email.Equals(_currentUser)
                              select o.Job_Id).Contains(c.JobId)
                      select c;
            var list = await job.ToListAsync().ConfigureAwait(false); 


            List<Job> jobs = new List<Job>();
            foreach (var x in list)
                jobs.Add(x);
            return View(jobs);
        }

        public async Task<ActionResult> GetMyAppliedJobs()
        {
            if (System.Web.HttpContext.Current.Session["Role"].ToString() != "Candidate")
                return View("~/Views/Shared/NoAcsess.cshtml");

            _currentUser = System.Web.HttpContext.Current.Session["UserName"].ToString();
            var myJobs = _db.AppliedJobs.Include(j => j.Candidate).Where(z=>z.Candidate.Email.Equals(_currentUser));
            if (myJobs == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(await myJobs.ToListAsync());
        }

        public async Task<ActionResult> MyProfile()
        {
            if (System.Web.HttpContext.Current.Session["Role"].ToString() != "Candidate")
                return View("~/Views/Shared/NoAcsess.cshtml");

            _currentUser = System.Web.HttpContext.Current.Session["UserName"].ToString();
            var candiadate =  await _db.Candidates.Where(z=>z.Email.Equals(_currentUser)).FirstOrDefaultAsync();
            if (candiadate == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            return View(candiadate);
        }

        public  async Task<ActionResult> Apply(int jobId)
        {
            if (System.Web.HttpContext.Current.Session["Role"].ToString() != "Candidate")
                return View("~/Views/Shared/NoAcsess.cshtml");

            _currentUser = System.Web.HttpContext.Current.Session["UserName"].ToString();
            var cand = await _db.Candidates.Where(z => z.Email.Equals(_currentUser)).FirstOrDefaultAsync();
            if (cand == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            AppliedJob appliedJob = new AppliedJob();
            appliedJob.Candiate_Id = cand.Id;
            appliedJob.Job_Id = jobId;
            if (ModelState.IsValid)
            {
                _db.AppliedJobs.Add(appliedJob);
                await _db.SaveChangesAsync();
                return RedirectToAction("GetAllJobs");
            }
            return RedirectToAction("GetAllJobs");
        }

        public async Task<ActionResult> GetRecruiterDetails(int recruiterId)
        {
            if (System.Web.HttpContext.Current.Session["Role"].ToString() != "Candidate")
                return View("~/Views/Shared/NoAcsess.cshtml");

            _currentUser = System.Web.HttpContext.Current.Session["UserName"].ToString();
            var recuriter = await _db.Recruiters.FindAsync(recruiterId);
            if(recuriter==null)
                return RedirectToAction("Index","Home");
            return View("~/Views/Recruiters/MyProfile.cshtml", recuriter);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
