using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.IO;
using System.Threading.Tasks;

namespace RedFolder.TagHelpers
{
    [HtmlTargetElement("rfc-inline-script")]
    public class RFCInlineScriptTagHelper : TagHelper
    {
        private readonly string _wwwRootPath;

        public RFCInlineScriptTagHelper(IWebHostEnvironment environment)
        {
            _wwwRootPath = environment.WebRootPath;
        }

        [HtmlAttributeName("js-file")]
        public string JsFile { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(JsFile))
            {
                var path = Path.Combine(_wwwRootPath, "scripts", JsFile);
                if (File.Exists(path))
                {
                    var content = await File.ReadAllTextAsync(path);
                    output.Content.SetHtmlContent($"<script>{content}</script>");
                }
            }
        }
    }
}
