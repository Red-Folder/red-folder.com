using Microsoft.AspNetCore.Razor.TagHelpers;
using Red_Folder.com.Utils;

namespace RedFolder.TagHelpers
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
