using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semester1Project.Models
{
    public class Rating
    {
        public string Id { get; set; }
        public double Value { get; set; }
        public string Text { get; set; }
        public virtual User userId { get; set; }
    }
}