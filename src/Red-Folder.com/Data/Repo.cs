using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Data
{
    public class Repo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public int OpenIssues { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
