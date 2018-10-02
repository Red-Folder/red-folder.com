using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.com.Utils
{
    public class TimePeriod
    {
        public static string FromSeconds(long seconds)
        {
            var timespan = TimeSpan.FromSeconds(seconds);

            var description = new StringBuilder();
            if (timespan.Hours > 0)
            {
                description.Append(timespan.Hours);
                if (timespan.Hours > 1)
                {
                    description.Append(" hours and ");
                }
                else
                {
                    description.Append(" hours and ");
                }
            }
            description.Append(timespan.Minutes);
            if (timespan.Minutes == 1)
            {
                description.Append(" minute");
            }
            else
            {
                description.Append(" minutes");
            }

            return description.ToString();
        }
    }
}
