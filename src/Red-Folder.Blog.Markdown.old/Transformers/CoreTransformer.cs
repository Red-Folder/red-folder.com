using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RedFolder.Blog.Markdown.Transformers
{
    public class CoreTransformer : BaseTransformer
    {
        protected override string PostTransform(JObject meta, string markdown)
        {
            HeyRed.MarkdownSharp.Markdown processor = new HeyRed.MarkdownSharp.Markdown();

            return processor.Transform(markdown);
        }
    }
}
