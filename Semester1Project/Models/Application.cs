using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
// asd
namespace Semester1Project.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }
        public string Status { get; set; }
        public virtual Job Job { get; set; }
        public virtual User User { get; set; }
    }
}