using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Red_Folder.com.TagHelpers
{
    public class TimePeriodTagHelper : TagHelper
    {
        [HtmlAttributeName("seconds")]
        public long Seconds { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var timespan = TimeSpan.FromSeconds(Seconds);

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

            output.Content.SetContent(description.ToString());
        }
    }
}
