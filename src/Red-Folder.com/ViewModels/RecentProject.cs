using System.Collections.Generic;

namespace RedFolder.ViewModels
{
    public class RecentProject
    {
        public string Title { get; set; }
        public List<string> Description { get; set; }
        public Diciplines Diciplines { get; set; }
        public List<string> Technologies { get; set; }
    }
}
