using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.IO;
using System.Threading.Tasks;

namespace RedFolder.TagHelpers
{
    [HtmlTargetElement("rfc-inline-style")]
    public class RFCInlineStyleTagHelper : TagHelper
    {
        private readonly string _wwwRootPath;

        public RFCInlineStyleTagHelper(IHostingEnvironment environment)
        {
            _wwwRootPath = environment.WebRootPath;
        }

        [HtmlAttributeName("css-file")]
        public string CssFile { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(CssFile))
            {
                var path = Path.Combine(_wwwRootPath, "css", CssFile);
                if (File.Exists(path))
                {
                    var content = await File.ReadAllTextAsync(path);
                    output.Content.SetHtmlContent($"<style>{content}</style>");
                }
            }
        }
    }
}
