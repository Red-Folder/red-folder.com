using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RedFolder.Website.Data;

namespace RedFolder.Blog.Markdown
{
    public class MarkdownTransformer: IMarkdownTransformer
    {
        public Website.Data.Blog Transform(JObject meta, string markdown)
        {
            return new Website.Data.Blog
            {
                Url = (string)meta["url"],
                Author = "Mark Taylor",
                Published = ConvertJsonToDateTime((string)meta["published"]),
                Modified = ConvertJsonToDateTime((string)meta["modified"]),
                Title = (string)meta["title"],
                Text = MarkdownToHtml(markdown),
                Enabled = Boolean.Parse((string)meta["enabled"]),

                Description = "RFC Weekly - a summary of things that I find interesting.  It is an indulgence; its the weekly update that I would like to receive.  Unfortunately no-one else is producing it so I figured I best get on with it.  Hopefully someone else will also find useful.",
                Image = @"https://www.red-folder.com/media/blog/rfc-weekly/RFCWeeklyTwitterCard.png",
                TwitterHandle = "@folderred"
            };
        }

        private string MarkdownToHtml(string markdown)
        {
            HeyRed.MarkdownSharp.Markdown processor = new HeyRed.MarkdownSharp.Markdown();

            return processor.Transform(markdown);
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
