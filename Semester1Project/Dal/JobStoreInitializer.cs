using Semester1Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semester1Project.Dal
{
    public class JobStoreInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<JobStoreContext>
    {
        protected override void Seed(JobStoreContext context)
        {
            List<User> Users = new List<User>();
            List<Job> Jobs = new List<Job>();

            User user1 = new User() { FullName = "John Smith", Email = "johnsmithcorp@jscorp.de", JobCount = 0, PhoneNum = "555123123", UserName = "johnnyboy", Pass = "qwe" };
            User user2 = new User() { FullName = "Johnny Test", Email = "jhonnytest@whatever.com", JobCount = 0, PhoneNum = "444123456", UserName = "jtest", Pass = "jtest" };
            User user3 = new User() { FullName = "James Johnson", Email = "jjs@johnsons.org", JobCount = 0, PhoneNum = "921555123", UserName = "jjs", Pass = "jjs" };
            User user4 = new User() { FullName = "Jane Doe", Email = "janedoe@doecorp.com", JobCount = 0, PhoneNum = "341234567", UserName = "jane", Pass = "jane" };

            Users.Add(user1);
            Users.Add(user2);
            Users.Add(user3);
            Users.Add(user4);

            Job job1 = new Job() { Date = new DateTime(2019, 11, 5), Description = "Walk My dog every day for a month", ExpectedHours = 2, JobCreator = user2, MaxWorkers = 1, Location = "Aarhus", Payment = 400, Title = "Walk my dog" };
            Job job2 = new Job() { Date = new DateTime(2019, 11, 5), Description = "Help me with my gardening", ExpectedHours = 2, JobCreator = user3, MaxWorkers = 1, Location = "Aarhus", Payment = 150, Title = "Help cutting grass" };
            Job job3 = new Job() { Date = new DateTime(2019, 11, 5), Description = "Wash my car", ExpectedHours = 2, JobCreator = user1, MaxWorkers = 1, Location = "Aarhus", Payment = 200, Title = "Help me wash my car inside and out" };

            Jobs.Add(job1);
            Jobs.Add(job2);
            Jobs.Add(job3);

            context.Users.Add(Users.FirstOrDefault());
            Jobs.ForEach(x => context.Jobs.Add(x));
            context.SaveChanges();
        }
    }
}