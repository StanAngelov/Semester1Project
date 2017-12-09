using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Semester1Project.Models
{
    public class AccDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}