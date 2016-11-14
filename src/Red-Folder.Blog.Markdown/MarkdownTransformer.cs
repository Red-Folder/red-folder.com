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
            _innerTransformer = acclaim;
        }

        public MarkdownTransformer(ITransformer innerTransformer)
        {
            _innerTransformer = innerTransformer;
        }

        public Website.Data.Blog Transform(JObject meta, string markdown)
        {
            return new Website.Data.Blog
            {
                Url = (string)meta["url"],
                Author = "Mark Taylor",
                Published = ConvertJsonToDateTime((string)meta["published"]),
                Modified = ConvertJsonToDateTime((string)meta["modified"]),
                Title = (string)meta["title"],
                Text = MarkdownToHtml(meta, markdown),
                Enabled = Boolean.Parse((string)meta["enabled"]),

                Description = "RFC Weekly - a summary of things that I find interesting.  It is an indulgence; its the weekly update that I would like to receive.  Unfortunately no-one else is producing it so I figured I best get on with it.  Hopefully someone else will also find useful.",
                Image = @"https://www.red-folder.com/media/blog/rfc-weekly/RFCWeeklyTwitterCard.png",
                TwitterHandle = "@folderred"
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
