using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.ViewModels
{
    public class HomePage
    {
        public List<RedFolder.ViewModels.SummaryTile> Content { get; set; }
        public ContactRequest Contact { get; set; }
    }
}
