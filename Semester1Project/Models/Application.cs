using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semester1Project.Models
{
    public class Application
    {
        public string Status { get; set; }
        public virtual Job Job { get; set; }
        public virtual User User { get; set; }
        
        public Application(Job job, User user)
        {
            Status = "New";
            Job = job;
            User = user;
        }
    }
}