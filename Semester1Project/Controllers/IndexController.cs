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
            string sess = Session["Username"].ToString();
            if (sess.Equals("") || sess == null)
            {
                return RegisterOrLogIn();
            }
            else
            {
                return Home();
            }
            
        }

        private ActionResult RegisterOrLogIn()
        {
            return View();
        }
        private ActionResult Home()
        {
            return View();
        }
    }
}