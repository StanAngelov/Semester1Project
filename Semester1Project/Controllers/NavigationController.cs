using Semester1Project.Dal;
using Semester1Project.Models;
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