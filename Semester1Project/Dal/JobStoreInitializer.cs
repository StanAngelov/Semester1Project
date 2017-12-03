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
        }
    }
}