using System.Collections.Generic;

namespace RedFolder.ViewModels
{
    public class Employment
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }

        public string Summary { get; set; }

        public List<string> Technologies { get; set; }
    }
}