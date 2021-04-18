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
    public class RecruitersController : Controller
    {
        private MyDBContext _db;
        private string _currentUser;
        public RecruitersController()
        {
            _db = new MyDBContext();
        }
        public async Task<ActionResult> MyJobs()
        {
            if (System.Web.HttpContext.Current.Session["Role"].ToString() != "Recruiter")
                return View("~/Views/Shared/NoAcsess.cshtml");
            _currentUser = System.Web.HttpContext.Current.Session["UserName"].ToString();
            var myJobs = _db.Jobs.Include(j => j.Recruiter).Where(z => z.Recruiter.Email.Equals(_currentUser));
            if(myJobs==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(await myJobs.ToListAsync());
        }
        public ActionResult PostJob()
        {
            if (System.Web.HttpContext.Current.Session["Role"].ToString() != "Recruiter")
                return View("~/Views/Shared/NoAcsess.cshtml");
            _currentUser = System.Web.HttpContext.Current.Session["UserName"].ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PostJob(Job job)
        {
            if (System.Web.HttpContext.Current.Session["Role"].ToString() != "Recruiter")
                return View("~/Views/Shared/NoAcsess.cshtml");
            _currentUser = System.Web.HttpContext.Current.Session["UserName"].ToString();
            job.Recruiter_ID = await _db.Recruiters.Where(z => z.Email.Equals(_currentUser)).Select(z => z.Id).FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                _db.Jobs.Add(job);
                await _db.SaveChangesAsync();
                return RedirectToAction("MyJobs");
            }
            return View(job);
        }

        public async Task<ActionResult> AppliedDetails(int jobId)
        {
            if (System.Web.HttpContext.Current.Session["Role"].ToString() != "Recruiter")
                return View("~/Views/Shared/NoAcsess.cshtml");
            var candidates = await _db.AppliedJobs.Include(z => z.Candidate).Include(z => z.Job).Where(z => z.Job_Id.Equals(jobId)).ToListAsync();
            if(candidates==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(candidates);
        }

        public async Task<ActionResult> MyProfile()
        {
            if (System.Web.HttpContext.Current.Session["Role"].ToString() != "Recruiter")
                return View("~/Views/Shared/NoAcsess.cshtml");
            _currentUser = System.Web.HttpContext.Current.Session["UserName"].ToString();
            var recruiter = await _db.Recruiters.Where(z => z.Email.Equals(_currentUser)).FirstOrDefaultAsync();
            if(recruiter==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(recruiter);
        }

        public async Task<ActionResult> ApplyCandidateDetails(int candiadateId)
        {
            if (System.Web.HttpContext.Current.Session["Role"].ToString() != "Recruiter")
                return View("~/Views/Shared/NoAcsess.cshtml");
            var candiadate = await _db.Candidates.FindAsync(candiadateId);
            if(candiadate==null)
                return RedirectToAction("Index","Home");
            return View("~/Views/Candidates/MyProfile.cshtml",candiadate);
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
