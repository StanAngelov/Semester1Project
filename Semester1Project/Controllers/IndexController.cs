using Semester1Project.Dal;
using Semester1Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Semester1Project.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
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
        #region REGISTER 
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public  ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                using(JobStoreContext db = new JobStoreContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();
                
                
            }
            return RedirectToAction("Login");
        }
        #endregion


        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            using(JobStoreContext db = new JobStoreContext())
            {
                
                var usr = db.Users.Where(u => u.UserName == user.UserName && u.Pass == user.Pass).FirstOrDefault();
                if(usr != null)
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
        #region SubmitJob

        public ActionResult SubmitJob()
        {
            //TODO make it work only when session is available !
            return View();
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
        #endregion
        #region MyOffers
        public ActionResult MyOffers()
        {
            List<Job> JobList = new List<Job>();
            using (JobStoreContext db = new JobStoreContext())
            {
                db.Jobs.Add(new Job { Description = "asdada" , Title= "title" , Location = "asdadas" ,});
                db.SaveChanges();
                 JobList = db.Jobs.ToList();
            }
                return View(JobList);
        }
        #endregion
        #region Search
        public Action
        #endregion
    }
}