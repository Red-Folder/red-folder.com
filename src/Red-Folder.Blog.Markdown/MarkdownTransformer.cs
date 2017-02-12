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
            var gist = new GistTransformer(image);
            _innerTransformer = gist;
        }

        public MarkdownTransformer(ITransformer innerTransformer)
        {
            _innerTransformer = innerTransformer;
        }

        public Website.Data.Blog Transform(JObject meta, string markdown)
        {
            try
            {
                var redirects = new List<Redirect>();
                if (meta["redirects"] != null)
                {
                    foreach (var redirect in meta["redirects"])
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

                var keyWords = new List<string>();
                if (meta["keyWords"] != null)
                {
                    foreach (var keyWord in meta["keyWords"])
                    {
                        keyWords.Add((string)keyWord);
                    }
                }

                return new Website.Data.Blog
                {
                    Id = (string)meta["id"],
                    Url = (string)meta["url"],
                    Author = "Mark Taylor",
                    Published = meta["published"].Value<DateTime>(),
                    Modified = meta["modified"].Value<DateTime>(),
                    Title = (string)meta["title"],
                    Text = MarkdownToHtml(meta, markdown),
                    Enabled = Boolean.Parse((string)meta["enabled"]),

                    Description = (string)meta["description"],
                    Image = (string)meta["image"],

                    Redirects = redirects,
                    KeyWords = keyWords,

                    Series = (string)meta["series"]
                };
            }
            catch (Exception ex)
            {
                // TODO - Need to add some logging
                return null;
            }
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
    }
}
