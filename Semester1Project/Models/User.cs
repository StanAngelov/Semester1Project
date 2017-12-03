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
        public int JobCount { get; set; }
        public string Location { get; set; }
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
        

        public User(string uName, string pass, string email, string phonenum, string realname, int jcount , string loc)
        {
            UserName = uName;
            Pass = pass;
            Email = email;
            PhoneNum = phonenum;
            RealName = realname;
            JobCount = jcount;
            Location = loc;
        }


    }
}