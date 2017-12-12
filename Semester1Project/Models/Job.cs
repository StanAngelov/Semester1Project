using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Semester1Project.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Required(ErrorMessage = "Title Required !")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description Required !")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Location Required !")]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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