using System;
using System.Collections.Generic;

namespace RedFolder.ViewModels
{
    public class Employment
    {
        public String From { get; private set; }
        public String To { get; private set; }
        public String Title { get; private set; }

        public String Summary { get; private set; }

        public List<string> Technologies { get; private set; }

        public Employment(string from, string to, string title)
        {
            From = from;
            To = to;
            Title = title;
        }

        public Employment (string from, string to, string title, string summary, List<string> technoligies)
        {
            From = from;
            To = to;
            Title = title;
            Summary = summary;
            Technologies = technoligies;
        }


    }
}