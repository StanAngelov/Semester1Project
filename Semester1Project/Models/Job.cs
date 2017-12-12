using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace Semester1Project.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }
        public int ExpectedHours { get; set; }
        public int Payment { get; set; }
        public bool IsDone { get; set; }
        public int MaxWorkers { get; set; }
        public virtual User JobCreator { get; set; }
        public virtual IEnumerable<User> Workers { get; set; }
        public virtual IEnumerable<Application> Applications { get; set; }
        public virtual IEnumerable<Tag> Tags { get; set; }
    }
}