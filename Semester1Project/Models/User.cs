using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semester1Project.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string RealName { get; set; }
        public int JobCount { get {  return Applications.Where(a => a.Status == "Completed").Count();   } }
        public string Location { get; set; }
        public virtual IEnumerable<Application> Applications { get; set; }
        public virtual IEnumerable<Rating> ListofRatings { get; set; }
        public double Rating
        {
            get
            {
                double sum = 0;
                int count = 0;

              foreach(Rating rate in ListofRatings)
                {
                    count++;
                    sum += rate.Value;
                }

                return sum / count;

            }
        }
        

        public User(string uName, string pass, string email, string phonenum, string realname , string loc)
        {
            UserName = uName;
            Pass = pass;
            Email = email;
            PhoneNum = phonenum;
            RealName = realname;          
            Location = loc;
        }


    }
}