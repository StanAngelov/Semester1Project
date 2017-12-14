using Semester1Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semester1Project.ViewModels
{
    public class UserApplicationViewModel
    {
        public User Applicant { get; set; }
        public Application Application { get; set; }
    }
}