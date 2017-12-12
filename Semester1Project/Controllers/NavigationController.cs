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
                return View();
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
            return View();
        }

        public ActionResult SubmitJob()
        {
            return View();
        }

        public ActionResult MyOffers()
        {
            List<Job> JobList = new List<Job>();
            using (JobStoreContext db = new JobStoreContext())
            {

                // todo fix this: JobList = db.Jobs.Where(c => c.JobCreator == Session["UserId"]).ToList();
            }
            return View(JobList);
        }

        public ActionResult UserProfile()
        {
            return View();
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
            //TODO make it work only when session is available !
            if (ModelState.IsValid)
            {
                using (JobStoreContext db = new JobStoreContext())
                {
                    db.Jobs.Add(job);
                    db.SaveChanges();

                }
                ModelState.Clear();

            }
            return View();
        }
    }
}