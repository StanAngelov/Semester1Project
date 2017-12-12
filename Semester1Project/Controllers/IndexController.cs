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
                using(AccDbContext db = new AccDbContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    var a = db.Users.ToList();
                }
                ModelState.Clear();
                
                ViewBag.Message = user.FullName + ", You have successfully registered !";
            }
            return View();
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
            using(AccDbContext db = new AccDbContext())
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
   
    }
}