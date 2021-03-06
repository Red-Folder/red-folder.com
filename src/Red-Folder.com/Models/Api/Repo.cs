﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Models.Api
{
    public class Repo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public int OpenIssues { get; set; }
        public List<String> Tags { get; set; }
    }
}
