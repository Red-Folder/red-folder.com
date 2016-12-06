using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RedFolder.Website.Data;
using RedFolder.Blog.Markdown.Transformers;

namespace RedFolder.Blog.Markdown
{
    public class MarkdownTransformer: IMarkdownTransformer
    {
        private ITransformer _innerTransformer;

        public MarkdownTransformer()
        {
            var core = new CoreTransformer();
            var code = new CodeTransformer(core);
            var acclaim = new AcclaimTransformer(code);
            var image = new ImageTransformer(acclaim);
            _innerTransformer = image;
        }

        public MarkdownTransformer(ITransformer innerTransformer)
        {
            _innerTransformer = innerTransformer;
        }

        public Website.Data.Blog Transform(JObject meta, string markdown)
        {
            var redirects = new List<Redirect>();
            if (meta["redirect"] != null)
            {
                foreach (var redirect  in meta["redirect"])
                {
                    redirects.Add(new Redirect
                    {
                        RedirectType = (string)redirect["redirectType"] == "301" ? System.Net.HttpStatusCode.MovedPermanently : System.Net.HttpStatusCode.TemporaryRedirect,
                        Url = (string)redirect["url"],
                        RedirectByRoute = Boolean.Parse((string)redirect["redirectByRoute"]),
                        RedirectByParameter = Boolean.Parse((string)redirect["redirectByParameter"])
                    });
                }
            }

            return new Website.Data.Blog
            {
                Id = (string)meta["id"],
                Url = (string)meta["url"],
                Author = "Mark Taylor",
                Published = ConvertJsonToDateTime((string)meta["published"]),
                Modified = ConvertJsonToDateTime((string)meta["modified"]),
                Title = (string)meta["title"],
                Text = MarkdownToHtml(meta, markdown),
                Enabled = Boolean.Parse((string)meta["enabled"]),

                Description = (string)meta["description"],
                Image = (string)meta["image"],

                Redirects = redirects
            };
        }

        private string MarkdownToHtml(JObject meta, string markdown)
        {
            if (_innerTransformer == null)
            {
                return markdown;
            }
            else
            {
                return _innerTransformer.TransformMarkdown(meta, markdown);
            }
        }

        private DateTime ConvertJsonToDateTime(string value)
        {
            if (value.Split('-').Length == 3)
            {
                var valueArray = value.Split('-');
                var year = int.Parse(valueArray[0]);
                var month = int.Parse(valueArray[1]);
                var day = int.Parse(valueArray[2]);

                return new DateTime(year, month, day);
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
}
