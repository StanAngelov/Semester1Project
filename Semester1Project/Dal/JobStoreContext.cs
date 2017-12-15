using Semester1Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Semester1Project.Dal
{
    public class JobStoreContext : DbContext
    {
        public JobStoreContext() : base("JobStoreContext") {
            Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}