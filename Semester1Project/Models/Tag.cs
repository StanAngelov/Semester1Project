﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semester1Project.Models
{
    public class Tag
    {
        public string Name { get; set; }
        public Tag(string name)
        {
            Name = name;
        }
    }
}