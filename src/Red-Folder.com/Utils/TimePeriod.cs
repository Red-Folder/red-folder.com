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
            var hours = (timespan.Days * 24) + timespan.Hours;
            if (hours > 0)
            {
                description.Append(hours);
                if (hours > 1)
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
