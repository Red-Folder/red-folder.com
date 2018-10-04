using Red_Folder.com.Models.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.ViewModels.Activity
{
    public class WeekSummary
    {
        public int Year { get; set; }

        public int WeekNumber { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public ActivityLayout<PodCastActivity> PodCasts { get; set; }

        public ActivityLayout<SkillsActivity> Skills { get; set; }

        public ActivityLayout<PluralsightActivity> Pluralsight { get; set; }

        public ActivityLayout<FocusActivity> Focus { get; set; }

        public ActivityLayout<ClientActivity> Clients { get; set; }
    }
}
