using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Red_Folder.com.Utils;

namespace Red_Folder.com.TagHelpers
{
    public class TimePeriodTagHelper : TagHelper
    {
        [HtmlAttributeName("seconds")]
        public long Seconds { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var description = TimePeriod.FromSeconds(Seconds);
            output.Content.SetContent(description);
        }
    }
}
