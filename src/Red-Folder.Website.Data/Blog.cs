﻿using System;
using System.Collections.Generic;

namespace RedFolder.Website.Data
{
    public class Blog
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public DateTime Published { get; set; }
        public DateTime Modified { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public string Text { get; set; }
        public List<string> KeyWords { get; set; }
        public List<Redirect> Redirects { get; set; }
    }
}
