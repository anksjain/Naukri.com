using Naukri.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Scrypt;

namespace Naukri.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private MyDBContext _db ;
        public AccountController()
        {
            _db = new MyDBContext();
        }
        public ActionResult CandidateLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CandidateLogin(LoginModel model)
        {
            var userExist=await _db.Candidates
                .Where(z=>z.Email.ToLower().Equals(model.Email.ToLower())).FirstOrDefaultAsync();
            if(userExist!=null)
            {
                ScryptEncoder encrypt = new ScryptEncoder();
                if (encrypt.Compare(model.Password, userExist.Password))
                {
                    Session["UserName"] = userExist.Email;
                    Session["Role"] = "Candidate";
                    return RedirectToAction("GetAllJobs", "Candidates");
                }
                
            }
            ModelState.AddModelError("", "Enter Correct UserId/Passowrd");
            return View(model);
        }

        public ActionResult RecruiterLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RecruiterLogin(LoginModel model)
        {
            var userExist = await _db.Recruiters
                .Where(z => z.Email.ToLower().Equals(model.Email.ToLower())).FirstOrDefaultAsync();
            if (userExist != null)
            {
                ScryptEncoder encrypt = new ScryptEncoder();
                if (encrypt.Compare(model.Password, userExist.Password))
                {
                    Session["UserName"] = userExist.Email;
                    Session["Role"] = "Recruiter";
                    return RedirectToAction("MyJobs", "Recruiters");
                }

            }
            ModelState.AddModelError("", "Enter Correct UserId/Passowrd");
            return View(model);
        }

        public ActionResult RecruiterRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RecruiterRegister(RecruiterRegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var userExist = await _db.Recruiters
                .Where(z => z.Email.ToLower().Equals(model.Email.ToLower())).FirstOrDefaultAsync();

                if (userExist==null)
                {
                    Recruiter recruiter = new Recruiter();
                    recruiter.Email = model.Email.ToLower();
                    recruiter.Company = model.Company;
                    recruiter.FirstName = model.FirstName;
                    recruiter.LastName = model.LastName;
                    recruiter.PhoneNumber = model.PhoneNumber;

                    ScryptEncoder encrypt = new ScryptEncoder();
                    recruiter.Password = encrypt.Encode(model.Password);
                    _db.Recruiters.Add(recruiter);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("RecruiterLogin", "Account");
                }
                ModelState.AddModelError("", "User already exist");
            }
            ModelState.AddModelError("", "Try Again");
            return View(model);
        }


        public ActionResult CandidateRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CandidateRegister(CandidateRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var userExist = await _db.Candidates
                .Where(z => z.Email.ToLower().Equals(model.Email.ToLower())).FirstOrDefaultAsync();

                if (userExist == null)
                {
                    Candidate candidate = new Candidate();
                    candidate.Email = model.Email.ToLower();
                    candidate.College = model.College;
                    candidate.FirstName = model.FirstName;
                    candidate.LastName = model.LastName;
                    candidate.PhoneNumber = model.PhoneNumber;
                    candidate.NoticePeriod = model.NoticePeriod;
                    candidate.DOB = model.DOB;

                    ScryptEncoder encrypt = new ScryptEncoder();
                    candidate.Password = encrypt.Encode(model.Password);
                    _db.Candidates.Add(candidate);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("CandidateLogin", "Account");
                }
                ModelState.AddModelError("", "User already exist");
                return View(model);
            }
            ModelState.AddModelError("", "Try Again");
            return View(model);
        }
        public ActionResult SignOut()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Home");

        }
    }
}