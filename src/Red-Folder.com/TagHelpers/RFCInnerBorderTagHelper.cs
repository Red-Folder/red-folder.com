using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace RedFolder.TagHelpers
{
    [HtmlTargetElement(Attributes = "rfc-inner-border")]
    public class RFCInnerBorderTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = output.Content.IsModified ? 
                                output.Content.GetContent() :
                                (await output.GetChildContentAsync()).GetContent();

            output.Content.SetHtmlContent(@"<div class=""rfc-panel-inner"">" +
                                            childContent +
                                            "</div>");
        }
    }
}
