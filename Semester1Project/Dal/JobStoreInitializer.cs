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

            Users.Add(new User() { FullName = "John Smith", Email = "johnsmithcorp@jscorp.de", JobCount = 5, PhoneNum = "555123123", UserName = "johnnyboy" , Pass= "qwe"});
            Jobs.Add(new Job() { Date = new DateTime(2019, 11, 5) , Description = "Walk My dog every day for a month" , ExpectedHours=2, JobCreator=Users.FirstOrDefault() , MaxWorkers=1 , Location="Aarhus", Payment=400 , Title = "Walk my dog"  } );
            Jobs.Add(new Job() { Date = new DateTime(2019, 11, 5), Description = "Help me with my gardening", ExpectedHours = 2, JobCreator = Users.FirstOrDefault(), MaxWorkers = 1, Location = "Aarhus", Payment = 150, Title = "Help cutting grass" });
            Jobs.Add(new Job() { Date = new DateTime(2019, 11, 5), Description = "Wash my car", ExpectedHours = 2, JobCreator = Users.FirstOrDefault(), MaxWorkers = 1, Location = "Aarhus", Payment = 200, Title = "Help me wash my car inside and out" });
            context.Users.Add(Users.FirstOrDefault());
            Jobs.ForEach(x => context.Jobs.Add(x));
            context.SaveChanges();
        }
    }
}