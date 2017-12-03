using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semester1Project.Models
{
    public class Job
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public int ExpectedHours { get; set; }
        public int Payment { get; set; }
        public bool IsDone { get; set; }
        public virtual IEnumerable<Application> Applications { get; set; }
        
        public Job(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}