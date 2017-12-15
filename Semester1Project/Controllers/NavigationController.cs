using Semester1Project.Dal;
using Semester1Project.Models;
using Semester1Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Semester1Project.Controllers
{
    public class NavigationController : Controller
    {
        // GET: MobileNavBar
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                using(JobStoreContext db = new JobStoreContext())
                {
                    List<Job> JobList = db.Jobs.Where(c => c.IsDone != true).ToList();
                    return View(JobList);
                }
                
           
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            using (JobStoreContext db = new JobStoreContext())
            {

                var usr = db.Users.Where(u => u.UserName == user.UserName && u.Pass == user.Pass).FirstOrDefault();
                if (usr != null)
                {
                    Session["UserId"] = usr.UserId;
                    Session["UserName"] = usr.UserName;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong");
                }
            }
            return View();
        }
        #endregion

        public ActionResult Search()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }

        public ActionResult SubmitJob()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult MarkDone(int id)
        {
            if (Session["UserId"] != null)
            {

                using(JobStoreContext db = new JobStoreContext())
                {
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    Job job = db.Jobs.Where(x => x.JobCreator.UserId == UserId && x.JobId == id).FirstOrDefault();
                    job.IsDone = true;

                    List<Application> app = db.Applications.Where(x => x.Job.JobCreator.UserId == UserId && x.Job.JobId == id).ToList();
                    app.ForEach(x => x.Status = "Done");
                    db.SaveChanges();

                }
                return RedirectToAction("MyOffers");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult UserDetail(int id)
        {
            using(JobStoreContext db = new JobStoreContext())
            {
                User user = new User();
                user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
                return View(user);
            }

           
        }
        
        public ActionResult JobApplicant(int id, int appid)
        {
            if (Session["UserId"] != null)
            {
                using (JobStoreContext db = new JobStoreContext())
                {
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    User user = new User();
                    user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
                    Application app = new Application();
                    app = db.Applications.Where(e => e.ApplicationId == appid && e.Job.JobCreator.UserId == UserId && e.User.UserId == user.UserId).FirstOrDefault();
                    UserApplicationViewModel viewmodel = new UserApplicationViewModel()
                    {
                        Applicant = user,
                        Application = app
                    };
                    if(app == null)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(viewmodel);
                    }
                 
                    
                }
            }
            else
            {
                return RedirectToAction("Login");
            }


        }
     

        public ActionResult MyOffers()
        {
            

               
            if (Session["UserId"] != null)
            {
                List<Job> JobList = new List<Job>();
                using (JobStoreContext db = new JobStoreContext())
                {
                    int UserId = Convert.ToInt32(Session["UserId"]);

                    JobList = db.Jobs.Where(c => c.JobCreator.UserId == UserId ).ToList();
                }
                return View(JobList);
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }

        public ActionResult UserProfile()
        {
            if (Session["UserId"] != null)
            {
                using (JobStoreContext db = new JobStoreContext())
                {
                    int userId = Convert.ToInt32(Session["UserId"]);
                    User user = db.Users.Where(c => c.UserId == userId).FirstOrDefault();
                    return View(user);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult UserProfile(User user)
        {
            if (Session["UserId"] != null)
            {
                using (JobStoreContext db = new JobStoreContext())
                {
                    int userId = Convert.ToInt32(Session["UserId"]);
                    User curUser = db.Users.Where(c => c.UserId == userId).FirstOrDefault();
                    curUser.UserName = user.UserName;
                    curUser.Pass = user.Pass;
                    curUser.PhoneNum = user.PhoneNum;
                    curUser.Email = user.Email;
                    curUser.FullName = user.FullName;
                    curUser.JobCount = user.JobCount;
                    curUser.Rating = user.Rating;
                    curUser.Location = user.Location;
                    db.SaveChanges();
                    return View(curUser);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Applicants(int id)
        {
            if (Session["UserId"] != null)
            {
                using (JobStoreContext db = new JobStoreContext())
            {
                Job job = db.Jobs.Where(c => c.JobId == id).FirstOrDefault();
                List<Application> applications = new List<Application>();
                applications = db.Applications.Where(a => a.Job.JobId == job.JobId).ToList();
                List<User> applicants = new List<User>();
                    List<UserApplicationViewModel> viewmodel = new List<UserApplicationViewModel>();
                    UserApplicationViewModel entry = new UserApplicationViewModel();
                    applications.ForEach(x => { entry.Application = x; entry.Applicant = x.User; viewmodel.Add(entry); });
                return View(viewmodel);
            }
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                using (JobStoreContext db = new JobStoreContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();


            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult SubmitJob(Job job)
        {
            if (Session["UserId"] != null)
            {
                if (ModelState.IsValid)
                {
                    
                    using (JobStoreContext db = new JobStoreContext())
                    {
                        int UserId = Convert.ToInt32(Session["UserId"]);
                        job.JobCreator = db.Users.Where(c => c.UserId == UserId).FirstOrDefault();
                        db.Jobs.Add(job);
                        db.SaveChanges();

                    }
                    ModelState.Clear();

                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
         
        }

        public ActionResult ChangeJob(int id)
        {
            if (Session["UserId"] != null)
            {
                using (JobStoreContext db = new JobStoreContext())
                {
                    Job job = db.Jobs.Where(c => c.JobId == id).FirstOrDefault();
                    return View("EditJob", job);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }




            }

        [HttpPost]
        public ActionResult ChangeJob(Job job)
        {
            if (Session["UserId"] != null)
            {
                using (JobStoreContext db = new JobStoreContext())
                {
                    Job dbJob = db.Jobs.Where(c => c.JobId == job.JobId).FirstOrDefault();
                    dbJob.Title = job.Title;
                    dbJob.Description = job.Description;
                    dbJob.Location = job.Location;
                    dbJob.Date = job.Date;
                    dbJob.ExpectedHours = job.ExpectedHours;
                    dbJob.Payment = job.Payment;
                    dbJob.MaxWorkers = job.MaxWorkers;
                    db.SaveChanges();
                    return RedirectToAction("MyOffers");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }        
        }

  

        public ActionResult DeleteJob(int id)
        {
            if (Session["UserId"] != null)
            {

                using (JobStoreContext db = new JobStoreContext())
                {
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    Job job = db.Jobs.Where(c => c.JobId == id && c.JobCreator.UserId == UserId).FirstOrDefault();
                    db.Jobs.Remove(job);
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("MyOffers");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult JobDetail(int id)
        {
            if (Session["UserId"] != null)
            {
                using (JobStoreContext db = new JobStoreContext())
                {
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    Job job = db.Jobs.Where(c => c.JobId == id ).FirstOrDefault();

                    if (job.JobCreator.UserId == UserId)
                    {
                        return RedirectToAction("ChangeJob/"+ job.JobId);
                    }
                    else
                    {
                        return View(job);
                    }
                }
            }
            else
            {
                return RedirectToAction("Login");
            }


        }

        public ActionResult Hire(int id)
        {
            if (Session["UserId"] != null)
            {
                using (JobStoreContext db = new JobStoreContext())
                {
                    int userId = Convert.ToInt32(Session["UserId"]);
                    Application app = new Application();
                    app = db.Applications.Where(x => x.ApplicationId == id && x.Job.JobCreator.UserId == userId).FirstOrDefault();
                    app.Status = "Started";
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Apply(int id)
        {
            if (Session["UserId"] != null)
            {
                using (JobStoreContext db = new JobStoreContext())
                {
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    Job job = db.Jobs.Where(c => c.JobId == id).FirstOrDefault();
                    User user = db.Users.Where(e => e.UserId == UserId).FirstOrDefault();
                    Application app = new Application()
                    {
                        Job = job,
                        User = user,
                        Status = "Pending"
                       
                    };
                    db.Applications.Add(app);
                    db.SaveChanges();
                    List<Job> JobList = db.Jobs.Where(c => c.IsDone != true).ToList();
                    return View("Index",JobList);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }


        }
    }
}