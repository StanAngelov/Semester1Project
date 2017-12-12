using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Semester1Project.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username Required !")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Required !")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Required(ErrorMessage ="Email Required !")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Please enter valid E-mail address")]
        public string Email { get; set; }

        public string PhoneNum { get; set; }

        [Required(ErrorMessage = "Full name Required !")]
        public string FullName { get; set; }
        public int JobCount { get; set; }
        public double Rating { get; set; }
        public string Location { get; set; }
        public virtual IEnumerable<Application> Applications { get; set; }
        public virtual IEnumerable<Rating> ListofRatings { get; set; }
       

    }
}